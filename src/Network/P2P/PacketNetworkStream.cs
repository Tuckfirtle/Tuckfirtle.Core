// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using TheDialgaTeam.Core.Tasks;

namespace Tuckfirtle.Core.Network.P2P
{
    public class PacketNetworkStream : IDisposable
    {
        private readonly NetworkStream _networkStream;

        private readonly byte[] _networkGuid;

        private readonly SemaphoreSlim _readSemaphoreSlim = new SemaphoreSlim(1, 1);

        private readonly SemaphoreSlim _writeSemaphoreSlim = new SemaphoreSlim(1, 1);

        public PacketNetworkStream(NetworkStream networkStream, NetworkType networkType)
        {
            _networkStream = networkStream;

            switch (networkType)
            {
                case NetworkType.Testnet:
                    _networkGuid = CoreConfiguration.TestnetNetworkGuid.ToByteArray();
                    break;

                case NetworkType.Mainnet:
                    _networkGuid = CoreConfiguration.MainnetNetworkGuid.ToByteArray();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null);
            }
        }

        public async Task<Packet> ReadPacketAsync(CancellationToken cancellationToken = default)
        {
            await _readSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                var readingTask = TaskState.Run<(NetworkStream, Func<Packet, bool>, CancellationToken), Packet>((_networkStream, VerifyPacket, cancellationToken), state =>
                {
                    var (networkStream, verifyPacketFunction, cancellationTokenState) = state;

                    do
                    {
                        try
                        {
                            var packet = Packet.Parser.ParseDelimitedFrom(networkStream);

                            if (!verifyPacketFunction(packet))
                                continue;

                            return packet;
                        }
                        catch (InvalidProtocolBufferException)
                        {
                            // If the packet is invalid or malformed, skip and retry again.
                        }
                    } while (!cancellationTokenState.CanBeCanceled || !cancellationTokenState.IsCancellationRequested);

                    return null;
                }, cancellationToken);

                var resultingTask = await Task.WhenAny(readingTask, Task.Delay(-1, cancellationToken)).ConfigureAwait(false);

                if (resultingTask != readingTask)
                    return null;

                var result = await readingTask.ConfigureAwait(false);
                return result;
            }
            finally
            {
                _readSemaphoreSlim.Release();
            }
        }

        public async Task WritePacketAsync(Packet packet, CancellationToken cancellationToken = default)
        {
            await _writeSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                var writingTask = TaskState.Run((packet, _networkStream), state =>
                {
                    var (packetState, networkStream) = state;
                    packetState.WriteDelimitedTo(networkStream);
                }, cancellationToken);

                await Task.WhenAny(writingTask, Task.Delay(-1, cancellationToken)).ConfigureAwait(false);
            }
            finally
            {
                _writeSemaphoreSlim.Release();
            }
        }

        private bool VerifyPacket(Packet packet)
        {
            // Step 1: Verify Packet Network
            if (packet.PacketNetwork.Length != _networkGuid.Length)
                return false;

            var packetNetwork = packet.PacketNetwork;

            for (var i = 0; i < 16; i++)
            {
                if (packetNetwork[i] == _networkGuid[i])
                    continue;

                return false;
            }

            // Step 2: Verify Packet Keep Alive Duration
            return packet.PacketKeepAliveDuration > 0;
        }

        public void Dispose()
        {
            _networkStream?.Dispose();
            _readSemaphoreSlim?.Dispose();
            _writeSemaphoreSlim?.Dispose();
        }
    }
}