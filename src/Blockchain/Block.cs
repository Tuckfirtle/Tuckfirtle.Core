// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Numerics;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Blockchain
{
    /// <summary>
    /// Class containing a blockchain block.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Block version.
        /// </summary>
        public byte Version { get; set; } = CoreSettings.BlockVersion;

        /// <summary>
        /// Block height.
        /// </summary>
        public ulong Height { get; set; } = 0;

        /// <summary>
        /// Block timestamp.
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        /// <summary>
        /// Block nonce.
        /// </summary>
        public ulong Nonce { get; set; } = 0;

        /// <summary>
        /// Block target pow value.
        /// </summary>
        public BigInteger TargetPowValue { get; set; } = DifficultyUtility.GetTargetPowValue(CoreSettings.GenesisBlockDifficulty);

        /// <summary>
        /// Block hash.
        /// </summary>
        public string BlockHash { get; set; } = string.Empty;

        /// <summary>
        /// Previous block hash.
        /// </summary>
        public string PreviousBlockHash { get; set; } = string.Empty;

        /// <summary>
        /// Merkle root hash.
        /// </summary>
        public string MerkleRootHash { get; set; } = string.Empty;

        public List<Transaction> Transactions { get; set; }
    }
}