// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Tuckfirtle.Core.Network.P2P.Header;

namespace Tuckfirtle.Core.Network.P2P
{
    public class PacketStream : IDisposable
    {
        private readonly NetworkStream _networkStream;

        private readonly NetworkType _networkType;

        private readonly byte[] _packetBuffer = new byte[CoreConfiguration.P2PReceiveBufferSize];

        private int _packetLength;

        private int _packetBufferOffset;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public PacketStream(NetworkStream networkStream, NetworkType networkType, int pingLimit)
        {
            _networkStream = networkStream;
            _networkType = networkType;
            _networkStream.ReadTimeout = CoreConfiguration.P2PPingPacketDuration + pingLimit;
            _networkStream.WriteTimeout = pingLimit;
        }

        public async Task<IPacketHeader> ReadPacketAsync(CancellationToken cancellationToken = default)
        {
            do
            {
                var packetHeader = new PacketHeader();

                _packetLength = 0;
                _packetBufferOffset = 0;

                try
                {
                    await FillPacketBufferAsync(packetHeader, 16, (statePacketHeader, stateBuffer) => { statePacketHeader.NetworkGuid = new Guid(stateBuffer); }).ConfigureAwait(false);

                    if (!packetHeader.VerifyNetworkGuid(_networkType))
                        continue;

                    await FillPacketBufferAsync(packetHeader, 1, (statePacketHeader, stateBuffer) => { statePacketHeader.NetworkProtocolVersion = stateBuffer[0]; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 1, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentCompressionType = (ContentCompressionType) stateBuffer[0]; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 1, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentEncryptionType = (ContentEncryptionType) stateBuffer[0]; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 1, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentType = (ContentType) stateBuffer[0]; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 1, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentChecksumType = (ContentChecksumType) stateBuffer[0]; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 4, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentLength = BitConverter.ToInt32(stateBuffer, 0); }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, 4, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentChecksumLength = BitConverter.ToInt32(stateBuffer, 0); }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, packetHeader.ContentLength, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentData = stateBuffer; }).ConfigureAwait(false);
                    await FillPacketBufferAsync(packetHeader, packetHeader.ContentChecksumLength, (statePacketHeader, stateBuffer) => { statePacketHeader.ContentChecksum = stateBuffer; }).ConfigureAwait(false);

                    if (!packetHeader.VerifyChecksum())
                        continue;

                    return packetHeader;
                }
                catch (SocketException)
                {
                    return null;
                }
                catch (EndOfStreamException)
                {
                    return null;
                }
                catch (Exception)
                {
                    // ignored
                }
            } while (!cancellationToken.IsCancellationRequested);

            return null;
        }

        public async Task WritePacketAsync(IPacketHeader packetHeader, CancellationToken cancellationToken = default)
        {
            var isExecute = false;

            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
                isExecute = true;

                using (var memoryStream = new MemoryStream())
                {
                    using (var binaryStream = new BinaryWriter(memoryStream))
                    {
                        binaryStream.Write(packetHeader.NetworkGuid.ToByteArray());
                        binaryStream.Write(packetHeader.NetworkProtocolVersion);
                        binaryStream.Write((byte) packetHeader.ContentCompressionType);
                        binaryStream.Write((byte) packetHeader.ContentEncryptionType);
                        binaryStream.Write((byte) packetHeader.ContentType);
                        binaryStream.Write((byte) packetHeader.ContentChecksumType);
                        binaryStream.Write(packetHeader.ContentLength);
                        binaryStream.Write(packetHeader.ContentChecksumLength);
                        binaryStream.Write(packetHeader.ContentData);
                        binaryStream.Write(packetHeader.ContentChecksum);
                        binaryStream.Flush();

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        var sendingBuffer = new byte[CoreConfiguration.P2PSendBufferSize];
                        int read;

                        do
                        {
                            read = await memoryStream.ReadAsync(sendingBuffer, 0, sendingBuffer.Length, cancellationToken).ConfigureAwait(false);
                            await _networkStream.WriteAsync(sendingBuffer, 0, sendingBuffer.Length, cancellationToken).ConfigureAwait(false);
                        } while (read > 0);
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (isExecute)
                    _semaphoreSlim.Release();
            }
        }

        private async Task FillPacketBufferAsync(PacketHeader packetHeader, int requireBufferSize, Action<PacketHeader, byte[]> bufferFullAction)
        {
            var offset = 0;
            var buffer = new byte[requireBufferSize];

            do
            {
                if (_packetBufferOffset >= _packetLength)
                {
                    _packetBufferOffset = 0;
                    _packetLength = await _networkStream.ReadAsync(_packetBuffer, 0, _packetBuffer.Length).ConfigureAwait(false);

                    if (_packetLength == 0)
                        throw new EndOfStreamException();
                }

                var remainingToFull = requireBufferSize - offset;
                var bufferRemaining = _packetLength - _packetBufferOffset;

                if (bufferRemaining >= remainingToFull)
                {
                    Buffer.BlockCopy(_packetBuffer, _packetBufferOffset, buffer, offset, remainingToFull);
                    bufferFullAction(packetHeader, buffer);
                    _packetBufferOffset += remainingToFull;
                    break;
                }

                Buffer.BlockCopy(_packetBuffer, _packetBufferOffset, buffer, offset, bufferRemaining);
                _packetBufferOffset += bufferRemaining;
                offset += bufferRemaining;
            } while (offset >= requireBufferSize);
        }

        public void Dispose()
        {
            _networkStream?.Dispose();
        }
    }
}