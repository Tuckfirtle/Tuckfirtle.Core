// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tuckfirtle.Core.Network.P2P.Header;

namespace Tuckfirtle.Core.Network.P2P
{
    public class PacketNetworkStream : IDisposable
    {
        private readonly NetworkStream _networkStream;

        private readonly NetworkType _networkType;

        private readonly SemaphoreSlim _readSemaphoreSlim = new SemaphoreSlim(1, 1);

        private readonly SemaphoreSlim _writeSemaphoreSlim = new SemaphoreSlim(1, 1);

        public PacketNetworkStream(NetworkStream networkStream, NetworkType networkType)
        {
            _networkStream = networkStream;
            _networkType = networkType;
        }

        public async Task<Packet> ReadPacketAsync(CancellationToken cancellationToken = default)
        {
            await _readSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                do
                {
                    var packet = Packet.Parser.ParseDelimitedFrom(_networkStream);

                    if (!VerifyPacket(packet))
                        continue;

                    return packet;
                } while (!cancellationToken.CanBeCanceled || !cancellationToken.IsCancellationRequested);
            }
            finally
            {
                _readSemaphoreSlim.Release();
            }

            return null;
        }

        public async Task WritePacketAsync(Packet packet, CancellationToken cancellationToken = default)
        {
            await _writeSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                packet.WriteDelimitedTo(_networkStream);
            }
            finally
            {
                _writeSemaphoreSlim.Release();
            }
        }

        private bool VerifyPacket(Packet packet)
        {
            // Step 1: Verify Packet Network
            switch (_networkType)
            {
                case NetworkType.Testnet:
                    if (!packet.PacketNetwork.SequenceEqual(CoreConfiguration.TestnetNetworkGuid.ToByteArray()))
                        return false;
                    break;

                case NetworkType.Mainnet:
                    if (!packet.PacketNetwork.SequenceEqual(CoreConfiguration.MainnetNetworkGuid.ToByteArray()))
                        return false;
                    break;

                default:
                    return false;
            }

            // Step 2: Verify Packet Network Protocol Version
            if (packet.PacketNetworkProtocolVersion > CoreConfiguration.P2PNetworkProtocolVersion)
                return false;

            // Step 3: Verify Packet Keep Alive Duration
            if (packet.PacketKeepAliveDuration <= 0)
                return false;

            // Step 4: Verify Packet Checksum
            switch (packet.PacketChecksumType)
            {
                case PacketChecksumType.None:
                    return true;

                case PacketChecksumType.Md5:
                    using (var md5 = new MD5CryptoServiceProvider())
                        return md5.ComputeHash(packet.PacketData.ToByteArray()).SequenceEqual(packet.PacketChecksum);

                case PacketChecksumType.Sha1:
                    using (var sha1 = new SHA1CryptoServiceProvider())
                        return sha1.ComputeHash(packet.PacketData.ToByteArray()).SequenceEqual(packet.PacketChecksum);

                case PacketChecksumType.Sha256:
                    using (var sha256 = new SHA256CryptoServiceProvider())
                        return sha256.ComputeHash(packet.PacketData.ToByteArray()).SequenceEqual(packet.PacketChecksum);

                case PacketChecksumType.Sha384:
                    using (var sha384 = new SHA384CryptoServiceProvider())
                        return sha384.ComputeHash(packet.PacketData.ToByteArray()).SequenceEqual(packet.PacketChecksum);

                case PacketChecksumType.Sha512:
                    using (var sha512 = new SHA512CryptoServiceProvider())
                        return sha512.ComputeHash(packet.PacketData.ToByteArray()).SequenceEqual(packet.PacketChecksum);

                default:
                    return false;
            }
        }

        public void Dispose()
        {
            _networkStream?.Dispose();
        }
    }
}