// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Transaction
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
        /// Sender's kem key.
        /// </summary>
        public string SenderKemKey { get; set; }

        /// <summary>
        /// Receiver's address.
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// Receiver's kem key.
        /// </summary>
        public string ReceiverKemKey { get; set; }

        /// <summary>
        /// Transaction data.
        /// </summary>
        public string TransactionEncryptedData { get; set; }
    }
}