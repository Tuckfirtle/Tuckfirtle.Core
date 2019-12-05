// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Text;
using Newtonsoft.Json;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Blockchain
{
    public class TransactionBuilder
    {
        private HashData<Transaction> Transaction { get; }

        public TransactionBuilder()
        {
            Transaction = new HashData<Transaction> { Data = new Transaction() };
        }

        public TransactionBuilder AddMinerOutput(string minerAddress, Block blockReference)
        {
            var timeDifference = DateTimeOffset.Now.ToUnixTimeSeconds() - blockReference.Timestamp;
            var expectedReward = (ulong) Math.Floor(CoreConfiguration.BlockRewardPerMinute * (timeDifference / 60.0m));

            Transaction.Data.TransactionOutputs.Add(new TransactionOutput
            {
                Amount = expectedReward,
                ReceiverAddress = minerAddress
            });

            return this;
        }

        public HashData<Transaction> BuildTransaction()
        {
            var jsonData = JsonConvert.SerializeObject(Transaction.Data, Formatting.None);
            var jsonBytes = Encoding.UTF8.GetBytes(jsonData);
            var hash = HashAlgorithmUtility.Sha256ComputeHash(jsonBytes);

            Transaction.Hash = BitConverter.ToString(hash).Replace("-", "");

            return Transaction;
        }
    }
}