// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Google.Protobuf;
using Tuckfirtle.Core.Network.P2P.Header;
using Tuckfirtle.Core.Network.P2P.Packets;

namespace Tuckfirtle.Core.Network.P2P
{
    public static class PacketUtility
    {
        public static async Task<Packet> SerializePacketAsync(IMessage message, NetworkType networkType, byte[] encryptionKey = null)
        {
            var packet = new Packet();

            switch (networkType)
            {
                case NetworkType.Testnet:
                    packet.PacketNetwork = ByteString.CopyFrom(CoreConfiguration.TestnetNetworkGuid.ToByteArray());
                    break;

                case NetworkType.Mainnet:
                    packet.PacketNetwork = ByteString.CopyFrom(CoreConfiguration.MainnetNetworkGuid.ToByteArray());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null);
            }

            packet.PacketNetworkProtocolVersion = CoreConfiguration.P2PNetworkProtocolVersion;
            packet.PacketKeepAliveDuration = CoreConfiguration.P2PKeepAliveDuration;
            packet.PacketCompressionType = CoreConfiguration.P2PPacketCompressionType;
            packet.PacketEncryptionType = message is HandshakePacket ? PacketEncryptionType.None : CoreConfiguration.P2PPacketEncryptionType;

            switch (message)
            {
                case PingPacket pingPacket:
                    packet.PacketType = PacketType.Ping;
                    break;

                case HandshakePacket handshakePacket:
                    packet.PacketType = PacketType.Handshake;
                    break;
            }

            packet.PacketChecksumType = CoreConfiguration.P2PPacketChecksumType;
            packet.PacketData = message.ToByteString();

            await EncryptPacketAsync(packet, encryptionKey).ConfigureAwait(false);
            await CompressPacketAsync(packet).ConfigureAwait(false);
            await GenerateChecksumAsync(packet).ConfigureAwait(false);

            return packet;
        }

        public static async Task<IMessage> DeserializePacketAsync(Packet packet, byte[] decryptionKey = null)
        {
            packet = await DecompressPacketAsync(packet).ConfigureAwait(false);
            packet = await DecryptPacketAsync(packet, decryptionKey).ConfigureAwait(false);

            switch (packet.PacketType)
            {
                case PacketType.Ping:
                    return PingPacket.Parser.ParseFrom(packet.PacketData);

                case PacketType.Handshake:
                    return HandshakePacket.Parser.ParseFrom(packet.PacketData);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task<Packet> CompressPacketAsync(Packet packet)
        {
            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    return packet;

                case PacketCompressionType.Deflate:
                    using (var memoryStream = new MemoryStream())
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
                    {
                        packet.PacketData.WriteTo(deflateStream);
                        packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                        return packet;
                    }

                case PacketCompressionType.Gzip:
                    using (var memoryStream = new MemoryStream())
                    using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                    {
                        packet.PacketData.WriteTo(gzipStream);
                        packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                        return packet;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task<Packet> DecompressPacketAsync(Packet packet)
        {
            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    return packet;

                case PacketCompressionType.Deflate:
                    using (var deflateStream = new DeflateStream(new MemoryStream(packet.PacketData.ToByteArray()), CompressionMode.Decompress))
                    {
                        packet.PacketData = await ByteString.FromStreamAsync(deflateStream).ConfigureAwait(false);
                        return packet;
                    }

                case PacketCompressionType.Gzip:
                    using (var gzipStream = new GZipStream(new MemoryStream(packet.PacketData.ToByteArray()), CompressionMode.Decompress))
                    {
                        packet.PacketData = await ByteString.FromStreamAsync(gzipStream).ConfigureAwait(false);
                        return packet;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task<Packet> EncryptPacketAsync(Packet packet, byte[] encryptionKey)
        {
            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    return packet;

                case PacketEncryptionType.Aes:
                    if (encryptionKey == null)
                        throw new SerializationException();

                    using (var aes = new AesCryptoServiceProvider())
                    {
                        aes.GenerateIV();
                        aes.Key = encryptionKey;

                        using (var memoryStream = new MemoryStream())
                        using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            await memoryStream.WriteAsync(aes.IV, 0, aes.IV.Length).ConfigureAwait(false);
                            packet.PacketData.WriteTo(cryptoStream);
                            cryptoStream.FlushFinalBlock();
                            packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                            return packet;
                        }
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task<Packet> DecryptPacketAsync(Packet packet, byte[] decryptionKey)
        {
            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    return packet;

                case PacketEncryptionType.Aes:
                    if (decryptionKey == null)
                        throw new SerializationException();

                    using (var aes = new AesCryptoServiceProvider())
                    {
                        aes.Key = decryptionKey;
                        aes.IV = packet.PacketData.Take(16).ToArray();

                        using (var cryptoStream = new CryptoStream(new MemoryStream(packet.PacketData.Skip(16).ToArray()), aes.CreateDecryptor(), CryptoStreamMode.Read))
                            packet.PacketData = await ByteString.FromStreamAsync(cryptoStream).ConfigureAwait(false);

                        return packet;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task<Packet> GenerateChecksumAsync(Packet packet)
        {
            switch (packet.PacketChecksumType)
            {
                case PacketChecksumType.None:
                    return packet;

                case PacketChecksumType.Md5:
                    using (var md5 = new MD5CryptoServiceProvider())
                    {
                        packet.PacketChecksum = ByteString.CopyFrom(md5.ComputeHash(packet.PacketData.ToByteArray()));
                        return packet;
                    }

                case PacketChecksumType.Sha1:
                    using (var sha1 = new SHA1CryptoServiceProvider())
                    {
                        packet.PacketChecksum = ByteString.CopyFrom(sha1.ComputeHash(packet.PacketData.ToByteArray()));
                        return packet;
                    }

                case PacketChecksumType.Sha256:
                    using (var sha256 = new SHA256CryptoServiceProvider())
                    {
                        packet.PacketChecksum = ByteString.CopyFrom(sha256.ComputeHash(packet.PacketData.ToByteArray()));
                        return packet;
                    }

                case PacketChecksumType.Sha384:
                    using (var sha384 = new SHA384CryptoServiceProvider())
                    {
                        packet.PacketChecksum = ByteString.CopyFrom(sha384.ComputeHash(packet.PacketData.ToByteArray()));
                        return packet;
                    }

                case PacketChecksumType.Sha512:
                    using (var sha512 = new SHA512CryptoServiceProvider())
                    {
                        packet.PacketChecksum = ByteString.CopyFrom(sha512.ComputeHash(packet.PacketData.ToByteArray()));
                        return packet;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}