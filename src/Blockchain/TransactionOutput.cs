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
        /// Transaction locking type.
        /// </summary>
        public TransactionLockingType LockingType { get; set; }

        /// <summary>
        /// Transaction locking data.
        /// </summary>
        public string LockingData { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}