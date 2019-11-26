// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;

namespace Tuckfirtle.Core
{
    /// <summary>
    /// Core settings class that contains settings for this blockchain.
    /// </summary>
    public static class CoreSettings
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
        public const ulong CoinAtomicUnit = 100;

        /// <summary>
        /// Current block version.
        /// </summary>
        public const byte BlockVersion = 1;

        /// <summary>
        /// Current transaction version.
        /// </summary>
        public const byte TransactionVersion = 1;

        /// <summary>
        /// Genesis block difficulty.
        /// </summary>
        public const int GenesisBlockDifficulty = 1000;

        /// <summary>
        /// Account address prefix.
        /// </summary>
        public const string AccountAddressPrefix = "TF";

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
        /// Remote procedural call (RPC) default port.
        /// </summary>
        public const ushort RPCDefaultPort = 15081;

        /// <summary>
        /// Unique testnet network id for network communication.
        /// </summary>
        public static Guid TestnetNetworkId = new Guid(new byte[] { 84, 117, 99, 107, 102, 105, 114, 116, 108, 101, 32, 84, 101, 115, 116, 32 });

        /// <summary>
        /// Unique mainnet network id for network communication.
        /// </summary>
        public static Guid MainnetNetworkId = new Guid(new byte[] { 84, 117, 99, 107, 102, 105, 114, 116, 108, 101, 32, 77, 97, 105, 110, 32 });
    }
}