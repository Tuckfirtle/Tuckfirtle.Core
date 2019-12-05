// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Blockchain
{
    public class MerkleHash
    {
        public string Hash { get; set; }

        public MerkleHash(string hash)
        {
            Hash = hash;
        }
    }
}