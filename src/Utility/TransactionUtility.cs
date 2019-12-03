// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using Tuckfirtle.Core.Blockchain;

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
        /// <returns>The transaction object.</returns>
        public static Transaction GenerateMinerTransaction(string address, Block blockReference)
        {
            var timeDifference = DateTimeOffset.Now.ToUnixTimeSeconds() - blockReference.Timestamp;
            var expectedReward = (ulong) Math.Floor(CoreConfiguration.BlockRewardPerMinute * (timeDifference / 60.0m));

            // TODO: Add the transaction with proper locking logic.

            return new Transaction
            {
                TransactionOutputs =
                {
                    new TransactionOutput
                    {
                        Amount = expectedReward,
                        LockingType = TransactionLockingType.P2PKH,
                        LockingData = ""
                    }
                }
            };
        }

        /// <summary>
        /// Generate a valid locking script for transaction output.
        /// </summary>
        /// <param name="lockingType">The type of locking script.</param>
        /// <param name="lockingVariables">The data for locking script.</param>
        public static string GenerateLockingScript(TransactionLockingType lockingType, params string[] lockingVariables)
        {
            // TODO: Complete locking data.

            switch (lockingType)
            {
                case TransactionLockingType.P2PK:
                    /*
                     * Locking script: Pay to public key.
                     *
                     * This script contain the public key of the receiver so that the receiver could unlock it.
                     *
                     * Expected variables:
                     * lockingVariables[0] = Public key of the receiver.
                     */
                    break;

                case TransactionLockingType.P2PKH:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(lockingType), lockingType, null);
            }

            return string.Empty;
        }
    }
}