// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Collections.Generic;

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
        public byte Version { get; set; } = CoreConfiguration.TransactionVersion;

        /// <summary>
        /// Transaction timestamp.
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        /// <summary>
        /// Transaction inputs.
        /// </summary>
        public List<TransactionInput> TransactionInputs { get; set; } = new List<TransactionInput>();

        /// <summary>
        /// Transaction outputs.
        /// </summary>
        public List<TransactionOutput> TransactionOutputs { get; set; } = new List<TransactionOutput>();
    }
}