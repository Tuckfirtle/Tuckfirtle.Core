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

        public byte[] PublicKeyHash { get; }

        private PublicKeyHashTransactionLocker(byte[] targetPublicKeyHash)
        {
            PublicKeyHash = targetPublicKeyHash;
        }

        public static ITransactionLocker CreateTransactionLocker(byte[] targetWalletAddress)
        {
            return new PublicKeyHashTransactionLocker(HashAlgorithmUtility.Sha256ComputeHash(targetWalletAddress));
        }

        public override string ToString()
        {
            return JsonUtility.Serialize(this);
        }
    }
}