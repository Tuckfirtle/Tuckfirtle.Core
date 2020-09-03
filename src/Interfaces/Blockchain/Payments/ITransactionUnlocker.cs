// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Interfaces.Blockchain.Payments
{
    /// <summary>
    /// This interface describe the contents of the transaction unlocking script.
    /// </summary>
    public interface ITransactionUnlocker
    {
        /// <summary>
        /// Transaction unlocker script name.
        /// </summary>
        string ScriptName { get; }

        /// <summary>
        /// Transaction unlocker signature.
        /// </summary>
        string Signature { get; }
    }
}