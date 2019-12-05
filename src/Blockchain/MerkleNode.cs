// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Blockchain
{
    public class MerkleNode
    {
        public string Hash { get; set; }

        public MerkleHash Left { get; set; }

        public MerkleHash Right { get; set; }
    }
}