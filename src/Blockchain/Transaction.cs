// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public List<TransactionInput> TransactionInputs { get; } = new List<TransactionInput>();

        /// <summary>
        /// Transaction outputs.
        /// </summary>
        public List<TransactionOutput> TransactionOutputs { get; } = new List<TransactionOutput>();

        /// <summary>
        /// Transaction hash.
        /// </summary>
        public string TransactionHash { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}