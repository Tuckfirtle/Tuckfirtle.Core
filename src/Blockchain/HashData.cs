// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Blockchain
{
    public class HashData<TData> where TData : class
    {
        public TData Data { get; set; }

        public string Hash { get; set; }
    }
}