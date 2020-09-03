// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.Core.Blockchain.Payments.Scripts.Locker;
using Tuckfirtle.Core.Interfaces.Blockchain.Payments;

namespace Tuckfirtle.Core.Blockchain.Payments
{
    public class TransactionOutput : ITransactionOutput
    {
        public ulong Amount { get; private set; }

        public ITransactionLocker LockingScript { get; private set; }

        /// <summary>
        /// Create a transaction output.
        /// </summary>
        /// <param name="target">Target address.</param>
        /// <param name="amount">Amount in atomic units.</param>
        /// <returns>Transaction output with locking script.</returns>
        public static ITransactionOutput CreateTransactionOutput(byte[] target, ulong amount)
        {
            return new TransactionOutput
            {
                Amount = amount,
                LockingScript = PublicKeyHashTransactionLocker.CreateTransactionLocker(target)
            };
        }
    }
}