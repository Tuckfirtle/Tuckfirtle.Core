// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
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

        public static Packet SerializePacket(IMessage message, NetworkType networkType, byte[]? encryptionKey = null)
        {
            var packet = new Packet
            {
                PacketNetwork = networkType switch
                {
                    NetworkType.Testnet => TestnetNetworkGuid,
                    NetworkType.Mainnet => MainnetNetworkGuid,
                    var _ => throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null)
                },
                PacketKeepAliveDuration = CoreConfiguration.P2PKeepAliveDuration,
                PacketCompressionType = CoreConfiguration.P2PPacketCompressionType,
                PacketEncryptionType = message is PingPacket || message is HandshakePacket ? PacketEncryptionType.None : CoreConfiguration.P2PPacketEncryptionType,
                PacketType = message switch
                {
                    PingPacket _ => PacketType.Ping,
                    HandshakePacket _ => PacketType.Handshake,
                    var _ => PacketType.Unknown
                }
            };

            if (message is PingPacket)
            {
                packet.PacketData = ByteString.Empty;
                return packet;
            }

            using var encryptedPacketMemoryStream = new MemoryStream();

            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    message.WriteTo(encryptedPacketMemoryStream);
                    break;

                case PacketEncryptionType.Aes:
                {
                    if (encryptionKey == null) throw new NullReferenceException($"{nameof(encryptionKey)} is null.");

                    using var aes = Aes.Create();
                    if (aes == null) throw new CryptographicException("Aes could not be initialized.");

                    aes.GenerateIV();
                    aes.Key = encryptionKey;

                    using var packetDataMemoryStream = new MemoryStream();
                    packetDataMemoryStream.WriteAsync(aes.IV, 0, 16);

                    using var cryptoStream = new CryptoStream(packetDataMemoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                    message.WriteTo(cryptoStream);
                    cryptoStream.FlushFinalBlock();

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    packet.PacketData = ByteString.FromStream(encryptedPacketMemoryStream);
                    break;

                case PacketCompressionType.Deflate:
                {
                    using var compressedPacketDataMemoryStream = new MemoryStream();
                    using var deflateStream = new DeflateStream(compressedPacketDataMemoryStream, CompressionLevel.Optimal);

                    encryptedPacketMemoryStream.WriteTo(deflateStream);
                    packet.PacketData = ByteString.FromStream(compressedPacketDataMemoryStream);

                    break;
                }

                case PacketCompressionType.Gzip:
                {
                    using var compressedPacketDataMemoryStream = new MemoryStream();
                    using var gzipStream = new GZipStream(compressedPacketDataMemoryStream, CompressionLevel.Optimal);

                    encryptedPacketMemoryStream.WriteTo(gzipStream);
                    packet.PacketData = ByteString.FromStream(compressedPacketDataMemoryStream);

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return packet;
        }

        public static T DeserializePacketData<T>(Packet packet, MessageParser<T> packetParser, byte[]? decryptionKey = null) where T : IMessage<T>
        {
            using var decompressedMemoryStream = new MemoryStream();

            switch (packet.PacketCompressionType)
            {
                case PacketCompressionType.None:
                    packet.PacketData.WriteTo(decompressedMemoryStream);
                    break;

                case PacketCompressionType.Deflate:
                {
                    using var compressedMemoryStream = new MemoryStream();
                    packet.PacketData.WriteTo(compressedMemoryStream);

                    using var deflateStream = new DeflateStream(compressedMemoryStream, CompressionMode.Decompress);
                    deflateStream.CopyTo(decompressedMemoryStream);

                    break;
                }

                case PacketCompressionType.Gzip:
                {
                    using var compressedMemoryStream = new MemoryStream();
                    packet.PacketData.WriteTo(compressedMemoryStream);

                    using var gZipStream = new GZipStream(compressedMemoryStream, CompressionMode.Decompress);
                    gZipStream.CopyTo(decompressedMemoryStream);

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (packet.PacketEncryptionType)
            {
                case PacketEncryptionType.None:
                    return packetParser.ParseDelimitedFrom(decompressedMemoryStream);

                case PacketEncryptionType.Aes:
                {
                    using var aes = Aes.Create();

                    if (aes == null) throw new CryptographicException("Aes could not be initialized.");

                    aes.Key = decryptionKey;

                    var ivBuffer = new byte[16];
                    decompressedMemoryStream.Read(ivBuffer, 0, 16);

                    aes.IV = ivBuffer;

                    using var cryptoStream = new CryptoStream(decompressedMemoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                    return packetParser.ParseDelimitedFrom(cryptoStream);
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}