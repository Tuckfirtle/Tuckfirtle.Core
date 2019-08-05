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
        public const ulong CoinAtomicUnit = 100000000;

        /// <summary>
        /// Coin total supply in atomic unit. (TBD)
        /// </summary>
        public const ulong CoinTotalSupply = 10000000000000000000;

        /// <summary>
        /// Current block version.
        /// </summary>
        public const int BlockVersion = 1;

        /// <summary>
        /// Genesis block difficulty.
        /// </summary>
        public const int GenesisBlockDifficulty = 1000;

        /// <summary>
        /// Current transaction version.
        /// </summary>
        public const int TransactionVersion = 1;

        /// <summary>
        /// Minimum transaction fee needed to make an transaction.
        /// </summary>
        public const ulong MinimumTransactionFee = 0;

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
        public const ushort P2PDefaultPort = 0;

        /// <summary>
        /// Remote procedural call (RPC) default port.
        /// </summary>
        public const ushort RPCDefaultPort = 0;

        /// <summary>
        /// Unique network id for network communication.
        /// </summary>
        public static Guid NetworkId = new Guid(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
    }
}