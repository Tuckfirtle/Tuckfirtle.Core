// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.Core.Interfaces.Blockchain.Payments;

namespace Tuckfirtle.Core.Blockchain.Payments
{
    public class TransactionInput : ITransactionInput
    {
        public string Reference { get; }

        public int OutputIndex { get; }

        public ITransactionUnlocker UnlockingScript { get; }
    }
}