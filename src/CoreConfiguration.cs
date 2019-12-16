// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using Tuckfirtle.Core.Network.P2P;
using Tuckfirtle.Core.Network.P2P.Header;

namespace Tuckfirtle.Core
{
    /// <summary>
    /// Core configuration class that contains settings for this blockchain.
    /// </summary>
    public static class CoreConfiguration
    {
        /// <summary>
        /// Coin full name.
        /// </summary>
        public const string CoinFullName = "Tuckfirtle";

        /// <summary>
        /// Coin ticker.
        /// </summary>
        public const string CoinTicker = "TF";

        /// <summary>
        /// Coin atomic unit.
        /// </summary>
        public const ulong CoinAtomicUnit = 100000000;

        /// <summary>
        /// Current block version.
        /// </summary>
        public const byte BlockVersion = 1;

        /// <summary>
        /// Block reward per minute in atomic units.
        /// </summary>
        public const ulong BlockRewardPerMinute = 1 * CoinAtomicUnit;

        /// <summary>
        /// Block minimum creation time in seconds.
        /// </summary>
        public const long BlockMinimumCreationTime = 1;

        /// <summary>
        /// Current transaction version.
        /// </summary>
        public const byte TransactionVersion = 1;

        /// <summary>
        /// Transaction minimum amount in atomic units.
        /// </summary>
        public const ulong TransactionMinimumAmount = 1;

        /// <summary>
        /// Genesis block difficulty.
        /// </summary>
        public const int GenesisBlockDifficulty = 1000;

        /// <summary>
        /// Tuckfirtle pow scratchpad size.
        /// </summary>
        public const int TuckfirtlePowScratchpadSize = 2 * 32 * 1024;

        /// <summary>
        /// Tuckfirtle pow memory loop round.
        /// </summary>
        public const int TuckfirtlePowMemoryLoopRound = 1;

        /// <summary>
        /// Peer to peer (P2P) default port.
        /// </summary>
        public const ushort P2PDefaultPort = 15080;

        /// <summary>
        /// Peer to peer (P2P) receive buffer size.
        /// </summary>
        public const int P2PReceiveBufferSize = 8 * 1024;

        /// <summary>
        /// Peer to peer (P2P) send buffer size.
        /// </summary>
        public const int P2PSendBufferSize = 8 * 1024;

        /// <summary>
        /// Peer to peer (P2P) ping packet duration.
        /// </summary>
        public const int P2PKeepAliveDuration = 30000;

        /// <summary>
        /// Peer to peer (P2P) network protocol version.
        /// </summary>
        public const int P2PNetworkProtocolVersion = 1;

        /// <summary>
        /// Peer to peer (P2P) packet compression type.
        /// </summary>
        public const PacketCompressionType P2PPacketCompressionType = PacketCompressionType.Gzip;

        /// <summary>
        /// Peer to peer (P2P) packet encryption type.
        /// </summary>
        public const PacketEncryptionType P2PPacketEncryptionType = PacketEncryptionType.Aes;

        /// <summary>
        /// Peer to peer (P2P) packet checksum type.
        /// </summary>
        public const PacketChecksumType P2PPacketChecksumType = PacketChecksumType.Sha256;

        /// <summary>
        /// Remote procedural call (RPC) default port.
        /// </summary>
        public const ushort RPCDefaultPort = 15081;

        /// <summary>
        /// Unique testnet network id for network communication.
        /// </summary>
        public static Guid TestnetNetworkGuid = new Guid(new byte[] { 84, 117, 99, 107, 102, 105, 114, 116, 108, 101, 32, 84, 101, 115, 116, 32 });

        /// <summary>
        /// Unique mainnet network id for network communication.
        /// </summary>
        public static Guid MainnetNetworkGuid = new Guid(new byte[] { 84, 117, 99, 107, 102, 105, 114, 116, 108, 101, 32, 77, 97, 105, 110, 32 });
    }
}