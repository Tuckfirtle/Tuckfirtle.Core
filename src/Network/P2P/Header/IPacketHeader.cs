// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;

namespace Tuckfirtle.Core.Network.P2P.Header
{
    public interface IPacketHeader
    {
        Guid NetworkGuid { get; }

        byte NetworkProtocolVersion { get; }

        ContentCompressionType ContentCompressionType { get; }

        ContentEncryptionType ContentEncryptionType { get; }

        ContentType ContentType { get; }

        ContentChecksumType ContentChecksumType { get; }

        int ContentLength { get; }

        int ContentChecksumLength { get; }

        byte[] ContentData { get; }

        byte[] ContentChecksum { get; }
    }
}