// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;

namespace Tuckfirtle.Core.Transaction
{
    /// <summary>
    /// Class containing transaction header information.
    /// </summary>
    public class TransactionHeader
    {
        /// <summary>
        /// Transaction version.
        /// </summary>
        public int Version { get; set; } = CoreSettings.TransactionVersion;

        /// <summary>
        /// Transaction timestamp.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Transaction input.
        /// </summary>
        public TransactionData[] Input { get; set; }

        /// <summary>
        /// Transaction output.
        /// </summary>
        public TransactionData[] Output { get; set; }
    }
}