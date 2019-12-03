// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Blockchain
{
    public enum TransactionLockingType
    {
        /// <summary>
        /// Pay to public key.
        /// </summary>
        P2PK = 0,

        /// <summary>
        /// Pay to public key hashed.
        /// </summary>
        P2PKH = 1
    }
}