// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Linq;
using System.Security.Cryptography;

namespace Tuckfirtle.Core.Network.P2P.Header
{
    /*
     * P2P Packet Specification
     *
     * ====================================================================================================
     * 1. PACKET HEADER
     * ====================================================================================================
     * Packet header consist of a few component to identify P2P communication.
     * The header should not be compressed.
     *
     * Packet header (29 bytes):
     * | Network Guid | Network Protocol Version | Content Compression Type | Content Encryption Type | Content Type | Content Checksum Type | Content Length | Content Checksum Length |
     * | 00..15 bytes | 16..16 bytes             | 17..17 bytes             | 18..18 bytes            | 19..19 bytes | 20..20 bytes          | 21..24 bytes   | 25..28 bytes
     *
     * Packet data:
     * | Content Data   | Content Checksum |
     * | Content Length | Checksum Length  |
     *
     * Network Guid (16 bytes): The current network.
     * Network Protocol Version (1 byte): The current network protocol version.
     * Content Compression Type (1 byte): The type of compression used for the content data.
     * Content Encryption Type (1 byte): The type of encryption used for the content data.
     * Content Type (1 byte): The type of content data.
     * Content Checksum Type (1 byte): The type of checksum algorithm used for the content data.
     * Content Length (4 bytes): The length of the content data.
     * Content Checksum Length (4 bytes): The length of the content checksum hash.
     * Content Data (Based on content length): The content data.
     * Content Checksum (Based on checksum type): The content data hash.
     */

    /// <summary>
    /// Peer to peer packet header structure.
    /// </summary>
    public class PacketHeader : IPacketHeader
    {
        private byte _networkProtocolVersion;
        private int _contentLength;
        private int _contentChecksumLength;

        public Guid NetworkGuid { get; set; }

        public byte NetworkProtocolVersion
        {
            get => _networkProtocolVersion;
            set
            {
                if (value > CoreConfiguration.P2PNetworkProtocolVersion)
                    throw new ArgumentOutOfRangeException(nameof(NetworkProtocolVersion), value, $"Network Protocol Version must be lower than or equal to {CoreConfiguration.P2PNetworkProtocolVersion}.");

                _networkProtocolVersion = value;
            }
        }

        public ContentCompressionType ContentCompressionType { get; set; }

        public ContentEncryptionType ContentEncryptionType { get; set; }

        public ContentType ContentType { get; set; }

        public ContentChecksumType ContentChecksumType { get; set; }

        public int ContentLength
        {
            get => _contentLength;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(ContentLength), value, "Content Length must be more than 0.");

                _contentLength = value;
            }
        }

        public int ContentChecksumLength
        {
            get => _contentChecksumLength;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(ContentChecksumLength), value, "Content Checksum Length must be more than 0.");

                _contentChecksumLength = value;
            }
        }

        public byte[] ContentData { get; set; }

        public byte[] ContentChecksum { get; set; }

        public bool VerifyNetworkGuid(NetworkType networkType)
        {
            switch (networkType)
            {
                case NetworkType.Testnet:
                    return NetworkGuid.Equals(CoreConfiguration.TestnetNetworkId);

                case NetworkType.Mainnet:
                    return NetworkGuid.Equals(CoreConfiguration.MainnetNetworkId);

                default:
                    throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null);
            }
        }

        public bool VerifyChecksum()
        {
            switch (ContentChecksumType)
            {
                case ContentChecksumType.Md5:
                    using (var hash = new MD5CryptoServiceProvider())
                        return hash.ComputeHash(ContentData).SequenceEqual(ContentChecksum);

                case ContentChecksumType.Sha256:
                    using (var hash = new SHA256Managed())
                        return hash.ComputeHash(ContentData).SequenceEqual(ContentChecksum);

                case ContentChecksumType.Sha512:
                    using (var hash = new SHA512Managed())
                        return hash.ComputeHash(ContentData).SequenceEqual(ContentChecksum);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}