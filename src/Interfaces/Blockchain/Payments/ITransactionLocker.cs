// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Interfaces.Blockchain.Payments
{
    /// <summary>
    /// This interface describe the contents of the transaction locking script.
    /// </summary>
    public interface ITransactionLocker
    {
        /// <summary>
        /// Transaction locker script name.
        /// </summary>
        string ScriptName { get; }
    }
}