// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Newtonsoft.Json;

namespace Tuckfirtle.Core.Blockchain
{
    /// <summary>
    /// Class containing a transaction output.
    /// </summary>
    public class TransactionOutput
    {
        /// <summary>
        /// Transaction amount.
        /// </summary>
        public ulong Amount { get; set; }

        /// <summary>
        /// Receiver wallet address.
        /// </summary>
        public string ReceiverAddress { get; set; }
    }
}