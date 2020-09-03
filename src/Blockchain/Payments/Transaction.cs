// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using Tuckfirtle.Core.Interfaces.Blockchain.Payments;

namespace Tuckfirtle.Core.Blockchain.Payments
{
    public class Transaction : ITransaction
    {
        public Guid NetworkIdentifier { get; private set; } = CoreConfiguration.MainnetNetworkGuid;

        public byte Version { get; } = CoreConfiguration.TransactionVersion;

        public long Timestamp { get; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public ITransactionInput[] Inputs { get; private set; }

        public ITransactionOutput[] Outputs { get; private set; }

        public byte[] Hash { get; private set; }

        /// <summary>
        /// Create a miner transaction.
        /// </summary>
        /// <param name="target">Target wallet address.</param>
        /// <param name="amount">Amount to pay in atomic unit.</param>
        /// <param name="networkIdentifier">Network identifier.</param>
        /// <returns>Miner's Transaction</returns>
        public static ITransaction CreateMinerTransaction(byte[] target, ulong amount, Guid networkIdentifier)
        {
            return new Transaction
            {
                NetworkIdentifier = networkIdentifier,
                Inputs = null,
                Outputs = new[] { TransactionOutput.CreateTransactionOutput(target, amount) }
            };
        }
    }
}