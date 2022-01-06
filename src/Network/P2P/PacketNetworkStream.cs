// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tuckfirtle.Core.Utility;

namespace Tuckfirtle.Core.Network.P2P
{
    public class PacketNetworkStream : IDisposable, IAsyncDisposable
    {
        private class ReadPacketState
        {
            private readonly NetworkStream _networkStream;
            private readonly byte[] _networkGuid;
            private readonly CancellationToken _cancellationToken;

            public ReadPacketState(NetworkStream networkStream, byte[] networkGuid, CancellationToken cancellationToken)
            {
                _networkStream = networkStream;
                _networkGuid = networkGuid;
                _cancellationToken = cancellationToken;
            }

            public Packet? Execute()
            {
                var networkStream = _networkStream;
                var networkGuid = _networkGuid;
                var cancellationToken = _cancellationToken;
                var packetParser = Packet.Parser;

                do
                {
                    try
                    {
                        var packet = packetParser.ParseDelimitedFrom(networkStream);
                        if (!packet.PacketNetwork.Span.SequenceEqual(networkGuid) || packet.PacketKeepAliveDuration < 1) continue;
                        return packet;
                    }
                    catch (InvalidProtocolBufferException)
                    {
                        // If the packet is invalid or malformed, skip and retry again.
                    }
                } while (!cancellationToken.IsCancellationRequested);

                return null;
            }
        }

        private class WritePacketState
        {
            private readonly NetworkStream _stream;
            private readonly Packet _packet;

            public WritePacketState(NetworkStream stream, Packet packet)
            {
                _stream = stream;
                _packet = packet;
            }

            public void Execute()
            {
                _packet.WriteDelimitedTo(_stream);
            }
        }

        private readonly NetworkStream _networkStream;

        private readonly byte[] _networkGuid;

        private readonly SemaphoreSlim _readSemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _writeSemaphoreSlim = new SemaphoreSlim(1, 1);

        private bool _isDisposed;

        public PacketNetworkStream(NetworkStream networkStream, NetworkType networkType)
        {
            _networkStream = networkStream;
            _networkGuid = networkType switch
            {
                NetworkType.Testnet => CoreConfiguration.TestnetNetworkGuid.ToByteArray(),
                NetworkType.Mainnet => CoreConfiguration.MainnetNetworkGuid.ToByteArray(),
                var _ => throw new ArgumentOutOfRangeException(nameof(networkType), networkType, null)
            };
        }

        /// <summary>
        /// Read packet from the network stream.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken" /> was canceled.</exception>
        public async Task<Packet?> ReadPacketAsync(CancellationToken cancellationToken = default)
        {
            await _readSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                var readPacketState = new ReadPacketState(_networkStream, _networkGuid, cancellationToken);
                Task<Packet?> readPacketTask = _networkStream.DataAvailable ? Task.Run(readPacketState.Execute, cancellationToken) : Task.Factory.StartNew(readPacketState.Execute, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                return await TaskUtility.WaitUntilCancellation(readPacketTask, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _readSemaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Write packet into the network stream.
        /// </summary>
        /// <param name="packet">Packet to write into the network stream.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken" /> was canceled.</exception>
        public async Task WritePacketAsync(Packet packet, CancellationToken cancellationToken = default)
        {
            await _writeSemaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                var writePacketState = new WritePacketState(_networkStream, packet);
                var writePacketTask = Task.Run(writePacketState.Execute, cancellationToken);
                await TaskUtility.WaitUntilCancellation(writePacketTask, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _writeSemaphoreSlim.Release();
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            Dispose(false);
        }

        public async ValueTask DisposeAsync()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            await _networkStream.DisposeAsync().ConfigureAwait(false);
            Dispose(true);
        }

        private void Dispose(bool asyncDispose)
        {
            if (!asyncDispose) _networkStream.Dispose();
            _readSemaphoreSlim.Dispose();
            _writeSemaphoreSlim.Dispose();
        }
    }
}