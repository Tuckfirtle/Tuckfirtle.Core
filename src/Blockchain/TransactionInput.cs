// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Newtonsoft.Json;

namespace Tuckfirtle.Core.Blockchain
{
    public class TransactionInput
    {
        /// <summary>
        /// Transaction reference.
        /// </summary>
        public string TransactionReference { get; set; }

        /// <summary>
        /// Transaction output index.
        /// </summary>
        public int TransactionOutputIndex { get; set; }

        /// <summary>
        /// Sender public key.
        /// </summary>
        public string SenderPublicKey { get; set; }

        /// <summary>
        /// Transaction signature.
        /// </summary>
        public string Signature { get; set; }
    }
}