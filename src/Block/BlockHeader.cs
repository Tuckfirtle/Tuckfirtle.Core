// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Numerics;
using Newtonsoft.Json;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Block
{
    /// <summary>
    /// Block header class that contains block header information.
    /// </summary>
    public class BlockHeader
    {
        /// <summary>
        /// Block version.
        /// </summary>
        public int Version { get; set; } = CoreSettings.BlockVersion;

        /// <summary>
        /// Block height.
        /// </summary>
        public ulong Height { get; set; } = 0;

        /// <summary>
        /// Block timestamp.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Block nonce.
        /// </summary>
        public ulong Nonce { get; set; } = 0;

        /// <summary>
        /// Block target pow value.
        /// </summary>
        public BigInteger TargetPowValue { get; set; } = DifficultyUtility.GetTargetPowValue(CoreSettings.GenesisBlockDifficulty);

        /// <summary>
        /// Previous block hash.
        /// </summary>
        public string PreviousBlockHash { get; set; } = string.Empty;

        /// <summary>
        /// Merkle root hash.
        /// </summary>
        public string MerkleRootHash { get; set; } = string.Empty;

        /// <summary>
        /// Convert block header to string representation.
        /// </summary>
        /// <returns>JSON string representation of block header.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}