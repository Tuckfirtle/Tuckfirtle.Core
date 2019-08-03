using System;

namespace Tuckfirtle.Core.Block
{
    public class Block
    {
        public int Version { get; set; }

        public ulong Height { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public ulong Nonce { get; set; }
    }
}