// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tuckfirtle.Core.Network.P2P.Header;

namespace Tuckfirtle.Core.Network.P2P.Packets
{
    public abstract class Packet
    {
        private readonly ContentType _contentType;

        protected Packet(ContentType contentType)
        {
            _contentType = contentType;
        }

        public async Task<IPacketHeader> SerializePacketAsync(NetworkType networkType, byte[] keyBytes = null)
        {
            var packetHeader = new PacketHeader();

            switch (networkType)
            {
                case NetworkType.Testnet:
                    packetHeader.NetworkGuid = CoreConfiguration.TestnetNetworkId;
                    break;

                case NetworkType.Mainnet:
                    packetHeader.NetworkGuid = CoreConfiguration.MainnetNetworkId;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null);
            }

            packetHeader.NetworkProtocolVersion = CoreConfiguration.P2PNetworkProtocolVersion;
            packetHeader.ContentCompressionType = CoreConfiguration.P2PContentCompressionType;

            if (_contentType == ContentType.HandshakeIn || _contentType == ContentType.HandshakeOut)
                packetHeader.ContentEncryptionType = ContentEncryptionType.None;
            else
                packetHeader.ContentEncryptionType = CoreConfiguration.P2PContentEncryptionType;

            packetHeader.ContentChecksumType = CoreConfiguration.P2PContentChecksumType;

            using (var encryptedMemoryStream = new MemoryStream())
            {
                switch (packetHeader.ContentEncryptionType)
                {
                    case ContentEncryptionType.None:
                        using (var streamWriter = new StreamWriter(encryptedMemoryStream))
                        {
                            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                            {
                                JsonSerializer.CreateDefault().Serialize(jsonTextWriter, this);
                                await streamWriter.FlushAsync().ConfigureAwait(false);
                                packetHeader.ContentData = encryptedMemoryStream.ToArray();
                            }
                        }
                        
                        break;

                    case ContentEncryptionType.Aes:
                        if (keyBytes == null)
                            throw new SerializationException();

                        using (var aes = new AesManaged())
                        {
                            aes.GenerateIV();
                            aes.Key = keyBytes;

                            await encryptedMemoryStream.WriteAsync(aes.IV, 0, aes.IV.Length).ConfigureAwait(false);

                            using (var cryptoStream = new CryptoStream(encryptedMemoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                using (var streamWriter = new StreamWriter(cryptoStream))
                                {
                                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                                    {
                                        JsonSerializer.CreateDefault().Serialize(jsonTextWriter, this);
                                        await streamWriter.FlushAsync().ConfigureAwait(false);

                                        if (!cryptoStream.HasFlushedFinalBlock)
                                            cryptoStream.FlushFinalBlock();

                                        packetHeader.ContentData = encryptedMemoryStream.ToArray();
                                    }
                                }
                            }
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            using (var compressedMemoryStream = new MemoryStream())
            {
                switch (packetHeader.ContentCompressionType)
                {
                    case ContentCompressionType.Deflate:
                        using (var deflateStream = new DeflateStream(compressedMemoryStream, CompressionLevel.Optimal))
                        {
                            await deflateStream.WriteAsync(packetHeader.ContentData, 0, packetHeader.ContentData.Length).ConfigureAwait(false);
                            await deflateStream.FlushAsync().ConfigureAwait(false);

                            packetHeader.ContentLength = Convert.ToInt32(compressedMemoryStream.Length);
                            packetHeader.ContentData = compressedMemoryStream.ToArray();
                        }
                            
                        break;

                    case ContentCompressionType.GZip:
                        using (var gzipStream = new GZipStream(compressedMemoryStream, CompressionLevel.Optimal))
                        {
                            await gzipStream.WriteAsync(packetHeader.ContentData, 0, packetHeader.ContentData.Length).ConfigureAwait(false);
                            await gzipStream.FlushAsync().ConfigureAwait(false);
                            
                            packetHeader.ContentLength = Convert.ToInt32(compressedMemoryStream.Length);
                            packetHeader.ContentData = compressedMemoryStream.ToArray();
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            switch (packetHeader.ContentChecksumType)
            {
                case ContentChecksumType.Md5:
                    using (var md5 = new MD5CryptoServiceProvider())
                    {
                        packetHeader.ContentChecksum = md5.ComputeHash(packetHeader.ContentData);
                        packetHeader.ContentChecksumLength = packetHeader.ContentChecksum.Length;
                    }
                    break;

                case ContentChecksumType.Sha256:
                    using (var sha256 = new SHA256Managed())
                    {
                        packetHeader.ContentChecksum = sha256.ComputeHash(packetHeader.ContentData);
                        packetHeader.ContentChecksumLength = packetHeader.ContentChecksum.Length;
                    }
                    break;

                case ContentChecksumType.Sha512:
                    using (var sha512 = new SHA512Managed())
                    {
                        packetHeader.ContentChecksum = sha512.ComputeHash(packetHeader.ContentData);
                        packetHeader.ContentChecksumLength = packetHeader.ContentChecksum.Length;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return packetHeader;
        }

        public async Task DeserializePacketAsync(IPacketHeader packetHeader, byte[] keyBytes = null)
        {
            if (_contentType != packetHeader.ContentType)
                throw new SerializationException();

            using (var decompressedDataMemoryStream = new MemoryStream())
            {
                switch (packetHeader.ContentCompressionType)
                {
                    case ContentCompressionType.Deflate:
                        using (var deflateStream = new DeflateStream(new MemoryStream(packetHeader.ContentData), CompressionMode.Decompress))
                            await deflateStream.CopyToAsync(decompressedDataMemoryStream).ConfigureAwait(false);
                        break;

                    case ContentCompressionType.GZip:
                        using (var gzipStream = new GZipStream(new MemoryStream(packetHeader.ContentData), CompressionMode.Decompress))
                            await gzipStream.CopyToAsync(decompressedDataMemoryStream).ConfigureAwait(false);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                decompressedDataMemoryStream.Seek(0, SeekOrigin.Begin);

                switch (packetHeader.ContentEncryptionType)
                {
                    case ContentEncryptionType.None:
                        using (var jsonTextReader = new JsonTextReader(new StreamReader(decompressedDataMemoryStream)))
                        {
                            JsonSerializer.CreateDefault().Populate(jsonTextReader, this);
                        }

                        break;

                    case ContentEncryptionType.Aes:
                        if (keyBytes == null)
                            throw new SerializationException();

                        try
                        {
                            using (var aes = new AesManaged())
                            {
                                aes.Key = keyBytes;

                                var ivKey = new byte[16];
                                await decompressedDataMemoryStream.ReadAsync(ivKey, 0, 16).ConfigureAwait(false);

                                aes.IV = ivKey;

                                using (var streamReader = new StreamReader(new CryptoStream(decompressedDataMemoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read)))
                                    JsonSerializer.CreateDefault().Populate(new JsonTextReader(streamReader), this);
                            }
                        }
                        catch (Exception)
                        {
                            throw new SerializationException();
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}