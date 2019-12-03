// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Collections.Generic;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Blockchain
{
    /// <summary>
    /// Class containing a blockchain account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account address.
        /// </summary>
        public string AccountAddress { get; set; }

        /// <summary>
        /// Account public spend key.
        /// </summary>
        public string PublicSpendKey { get; set; }

        /// <summary>
        /// Account private spend key.
        /// </summary>
        public string PrivateSpendKey { get; set; }

        /// <summary>
        /// Account amount.
        /// </summary>
        public ulong Amount { get; set; }

        /// <summary>
        /// Account nonce.
        /// </summary>
        public ulong Nonce { get; set; }

        /// <summary>
        /// Account transaction reference.
        /// </summary>
        public List<Transaction> TransactionReference { get; set; }

        public Account()
        {
            AsymmetricAlgorithmUtility.GenerateKeypair(out var publicKey, out var privateKey);

            AccountAddress = Base58Utility.Encode(publicKey);
            PublicSpendKey = BitConverter.ToString(publicKey).Replace("-", "");
            PrivateSpendKey = BitConverter.ToString(privateKey).Replace("-", "");
        }
    }
}