// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;

namespace Tuckfirtle.Core.Blockchain
{
    /// <summary>
    /// Class containing a blockchain transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Transaction version.
        /// </summary>
        public int Version { get; set; } = CoreSettings.TransactionVersion;

        /// <summary>
        /// Transaction timestamp.
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        /// <summary>
        /// Transaction input.
        /// </summary>
        public TransactionData[] Input { get; set; }

        /// <summary>
        /// Transaction output.
        /// </summary>
        public TransactionData[] Output { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        public string TransactionHash { get; set; }
    }
}