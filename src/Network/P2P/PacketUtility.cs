// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.IO;
using System.IO.Compression;
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
        private static readonly ByteString TestnetNetworkGuid;

        private static readonly ByteString MainnetNetworkGuid;

        static PacketUtility()
        {
            TestnetNetworkGuid = ByteString.CopyFrom(CoreConfiguration.TestnetNetworkGuid.ToByteArray());
            MainnetNetworkGuid = ByteString.CopyFrom(CoreConfiguration.MainnetNetworkGuid.ToByteArray());
        }

        public static async Task<Packet> SerializePacketAsync(IMessage message, NetworkType networkType, byte[] encryptionKey = null)
        {
            var packet = new Packet
            {
                PacketNetwork = GetPacketNetwork(networkType),
                PacketKeepAliveDuration = CoreConfiguration.P2PKeepAliveDuration,
                PacketCompressionType = CoreConfiguration.P2PPacketCompressionType,
                PacketEncryptionType = GetPacketEncryptionTypeFromMessage(message),
                PacketType = GetPacketTypeFromMessage(message),
                PacketData = message.ToByteString()
            };

            await EncryptPacketAsync(packet, encryptionKey).ConfigureAwait(false);
            CompressPacket(packet);

            return packet;
        }

        public static async Task<IMessage> DeserializePacketAsync(Packet packet, byte[] decryptionKey = null)
        {
            await DecompressPacketAsync(packet).ConfigureAwait(false);
            await DecryptPacketAsync(packet, decryptionKey).ConfigureAwait(false);

            switch (packet.PacketType)
            {
                case PacketType.Unknown:
                    throw new SerializationException();

                case PacketType.Ping:
                    return PingPacket.Parser.ParseFrom(packet.PacketData);

                case PacketType.Handshake:
                    return HandshakePacket.Parser.ParseFrom(packet.PacketData);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ByteString GetPacketNetwork(NetworkType networkType)
        {
            switch (networkType)
            {
                case NetworkType.Testnet:
                    return TestnetNetworkGuid;

                case NetworkType.Mainnet:
                    return MainnetNetworkGuid;

                default:
                    throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null);
            }
        }

        private static PacketEncryptionType GetPacketEncryptionTypeFromMessage(IMessage message)
        {
            return message is HandshakePacket ? PacketEncryptionType.None : PacketEncryptionType.Aes;
        }

        private static PacketType GetPacketTypeFromMessage(IMessage message)
        {
            switch (message)
            {
                case PingPacket _:
                    return PacketType.Ping;

                case HandshakePacket _:
                    return PacketType.Handshake;

                default:
                    return PacketType.Unknown;
            }
        }

        private static void CompressPacket(Packet packet)
        {
            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    break;

                case PacketCompressionType.Deflate:
                    using (var memoryStream = new MemoryStream())
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
                    {
                        packet.PacketData.WriteTo(deflateStream);
                        packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                    }

                    break;

                case PacketCompressionType.Gzip:
                    using (var memoryStream = new MemoryStream())
                    using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                    {
                        packet.PacketData.WriteTo(gzipStream);
                        packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task DecompressPacketAsync(Packet packet)
        {
            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    break;

                case PacketCompressionType.Deflate:
                    using (var deflateStream = new DeflateStream(new MemoryStream(packet.PacketData.ToByteArray()), CompressionMode.Decompress))
                        packet.PacketData = await ByteString.FromStreamAsync(deflateStream).ConfigureAwait(false);

                    break;

                case PacketCompressionType.Gzip:
                    using (var gzipStream = new GZipStream(new MemoryStream(packet.PacketData.ToByteArray()), CompressionMode.Decompress))
                        packet.PacketData = await ByteString.FromStreamAsync(gzipStream).ConfigureAwait(false);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task EncryptPacketAsync(Packet packet, byte[] encryptionKey)
        {
            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    break;

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
                            await memoryStream.WriteAsync(aes.IV, 0, 16).ConfigureAwait(false);
                            packet.PacketData.WriteTo(cryptoStream);
                            cryptoStream.FlushFinalBlock();
                            packet.PacketData = ByteString.CopyFrom(memoryStream.ToArray());
                        }
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task DecryptPacketAsync(Packet packet, byte[] decryptionKey)
        {
            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    break;

                case PacketEncryptionType.Aes:
                    if (decryptionKey == null)
                        throw new SerializationException();

                    using (var aes = new AesCryptoServiceProvider())
                    {
                        aes.Key = decryptionKey;
                        aes.IV = packet.PacketData.Span.Slice(0, 16).ToArray();

                        using (var cryptoStream = new CryptoStream(new MemoryStream(packet.PacketData.Span.Slice(16).ToArray()), aes.CreateDecryptor(), CryptoStreamMode.Read))
                            packet.PacketData = await ByteString.FromStreamAsync(cryptoStream).ConfigureAwait(false);

                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}