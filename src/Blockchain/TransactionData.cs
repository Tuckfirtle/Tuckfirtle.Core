// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Blockchain
{
    public class TransactionData
    {
        /// <summary>
        /// Transaction version.
        /// </summary>
        public int Version { get; set; } = CoreSettings.TransactionVersion;

        /// <summary>
        /// Sender's address.
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// Sender's signature.
        /// </summary>
        public string SenderSignature { get; set; }

        /// <summary>
        /// Receiver's address.
        /// </summary>
        public string ReceiverAddress { get; set; }
    }
}