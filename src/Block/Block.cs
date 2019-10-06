// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System.Collections.Generic;

namespace Tuckfirtle.Core.Block
{
    /// <summary>
    /// Class containing a block.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Block header information.
        /// </summary>
        public BlockHeader BlockHeader { get; set; }

        public List<Transaction.Transaction> Transactions { get; set; }
    }
}