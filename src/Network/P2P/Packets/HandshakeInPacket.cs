// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.Core.Network.P2P.Header;

namespace Tuckfirtle.Core.Network.P2P.Packets
{
    public class HandshakeInPacket : Packet
    {
        public byte[] PublicKey { get; set; }

        public HandshakeInPacket() : base(ContentType.HandshakeIn)
        {
        }
    }
}