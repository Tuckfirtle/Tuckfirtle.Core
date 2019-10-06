// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Transaction
{
    /// <summary>
    /// Class containing a transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Transaction header.
        /// </summary>
        public TransactionHeader TransactionHeader { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        public string Hash { get; set; }
    }
}