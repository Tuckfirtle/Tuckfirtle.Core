// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

namespace Tuckfirtle.Core.Network.P2P.Header
{
    public enum ContentEncryptionType
    {
        None = 0,
        SikeP434Compressed = 1,
        SikeP503Compressed = 2,
        SikeP610Compressed = 3,
        SikeP751Compressed = 4
    }
}