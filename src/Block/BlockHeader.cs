using System;

namespace Tuckfirtle.Core.Block
{
    public class BlockHeader
    {
        public byte Version { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public ulong Nonce { get;set; }

        public ulong DifficultyTarget { get; set; }
    }
}