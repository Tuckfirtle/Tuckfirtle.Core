// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.Core.Interfaces.Blockchain.Payments;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Blockchain.Payments.Scripts.Locker
{
    public class PublicKeyHashTransactionLocker : ITransactionLocker
    {
        public string ScriptName { get; } = nameof(PublicKeyHashTransactionLocker);

        public byte[] PublicKeyHash { get; private set; }

        public static ITransactionLocker CreateTransactionLocker(byte[] target)
        {
            return new PublicKeyHashTransactionLocker
            {
                PublicKeyHash = HashAlgorithmUtility.Sha256ComputeHash(target)
            };
        }
    }
}