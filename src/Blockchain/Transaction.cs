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
        public byte Version { get; set; } = CoreSettings.TransactionVersion;

        /// <summary>
        /// Transaction timestamp.
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        /// <summary>
        /// Transaction sender address.
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// Transaction receiver address.
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public ulong Amount { get; set; }

        /// <summary>
        /// Transaction account nonce.
        /// </summary>
        public ulong AccountNonce { get; set; }

        /// <summary>
        /// Transaction signature.
        /// </summary>
        public string TransactionSignature { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        public string TransactionHash { get; set; }
    }
}