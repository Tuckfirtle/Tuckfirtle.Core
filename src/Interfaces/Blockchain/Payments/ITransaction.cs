// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;

namespace Tuckfirtle.Core.Interfaces.Blockchain.Payments
{
    /// <summary>
    /// This interface describe the contents of the transaction.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Transaction network identifier.
        /// </summary>
        Guid NetworkIdentifier { get; }

        /// <summary>
        /// Transaction version.
        /// </summary>
        byte Version { get; }

        /// <summary>
        /// Transaction timestamp.
        /// </summary>
        long Timestamp { get; }

        /// <summary>
        /// Transaction inputs.
        /// </summary>
        ITransactionInput[] Inputs { get; }

        /// <summary>
        /// Transaction outputs.
        /// </summary>
        ITransactionOutput[] Outputs { get; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        byte[] Hash { get; }
    }
}