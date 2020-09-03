// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using Tuckfirtle.Core.Blockchain;
using Tuckfirtle.Core.Blockchain.Payments;
using Tuckfirtle.Core.Interfaces.Blockchain;
using Tuckfirtle.Core.Interfaces.Blockchain.Payments;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all transaction functions.
    /// </summary>
    public static class TransactionUtility
    {
        /// <summary>
        /// Generate a unique transaction that will be accepted for each block for rewarding miners.
        /// </summary>
        /// <param name="address">Wallet address this transaction goes to.</param>
        /// <param name="blockReference">Reference to the previous block you are mining for.</param>
        public static ITransaction GenerateMinerTransaction(string address, Block blockReference)
        {
            var timeDifference = DateTimeOffset.Now.ToUnixTimeSeconds() - blockReference.Timestamp;
            var expectedReward = (ulong) Math.Floor(CoreConfiguration.BlockRewardPerTimeUnit * (timeDifference / 60.0m));

            return new Transaction();
        }
    }
}