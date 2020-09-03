// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Interfaces.Blockchain.Payments
{
    /// <summary>
    /// This interface describe the contents of the transaction input.
    /// </summary>
    public interface ITransactionInput
    {
        /// <summary>
        /// Transaction reference.
        /// </summary>
        string Reference { get; }

        /// <summary>
        /// Transaction output index.
        /// </summary>
        int OutputIndex { get; }
    }
}