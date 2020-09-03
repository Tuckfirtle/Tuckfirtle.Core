// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Interfaces.Blockchain.Payments
{
    /// <summary>
    /// This interface describe the contents of the transaction output.
    /// </summary>
    public interface ITransactionOutput
    {
        /// <summary>
        /// Transaction output atomic amount.
        /// </summary>
        ulong Amount { get; }

        /// <summary>
        /// Transaction output locking script.
        /// </summary>
        ITransactionLocker LockingScript { get; }
    }
}