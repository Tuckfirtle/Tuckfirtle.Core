// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.Core.Interfaces.Blockchain.Payments;

namespace Tuckfirtle.Core.Blockchain.Payments.Scripts.Unlocker
{
    public class PublicKeyHashTransactionUnlocker : ITransactionUnlocker
    {
        public string ScriptName { get; } = nameof(PublicKeyHashTransactionUnlocker);

        public byte[] PublicKey { get; private set; }

        public string Signature { get; private set; }
    }
}