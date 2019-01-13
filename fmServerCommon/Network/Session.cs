using System;
using fmLibrary;
using System.Net.Sockets;
using fmCommon;

namespace fmServerCommon
{
    // BUFFER_SIZE, HEADER_SIZE를 공용을 사용해야한다.
    public class Session : SessionBase
    {
        // BUFFER_SIZE, HEADER_SIZE를 공용을 사용
        private static readonly int BUFFER_SIZE = Packet.BUFFER_SIZE;   //8192 * 2;
        private static readonly int HEADER_SIZE = Packet.HEADER_SIZE;   //8;        // header (8byte) = size (4byte) + type (4byte)

        private byte[] m_buffer = new byte[BUFFER_SIZE];
        private int m_bufferPos = 0;

        ~Session() { }

        public override void Dispose()
        {
            m_buffer = null;
            base.Dispose();
        }

        public Session(Socket socket, SocketAsyncEventArgs recvSAEA, SocketAsyncEventArgs sendSAEA, PooledBufferManager pooledBufferManager)
            : base(socket, recvSAEA, sendSAEA, pooledBufferManager)
        {
        }

        public virtual void SendPacket(fmProtocol fp)
        {
            if (null == fp) return;

            using (Packet p = new Packet())
            {
                fp.Serialize(p);
                Send(p.GetBuffer(), 0, p.GetPacketLen());
            }
        }

        public virtual void RelayPacket(Packet p)
        {
            if (null == p) return;

            Send(p.GetBuffer(), 0, p.GetPacketLen());
        }

        protected override void OnReceived(byte[] buffer, int offset, int length)
        {
            if (m_buffer.Length <= length || m_buffer.Length <= m_bufferPos + length)
            {
                Logger.Debug("Invalid Receive Data");
                ForceDisconnect(CloseReason.InvalidReceiveData);
                return;
            }

            Array.Copy(buffer, offset, m_buffer, m_bufferPos, length);
            m_bufferPos += length;

            if (m_bufferPos < HEADER_SIZE)
            {
                Logger.Debug("Invalid Packet Header");
                ForceDisconnect(CloseReason.InvalidPacketHeader);
                return;
            }

            int remainSize = m_bufferPos;
            int readOffset = 0;

            while (0 < remainSize)
            {
                int packetSize = BitConverter.ToInt32(m_buffer, readOffset);

                if (packetSize < 0 || BUFFER_SIZE <= packetSize)
                {
                    Logger.Debug("Invalid OutOfPacketSize");
                    ForceDisconnect(CloseReason.InvalidOutOfPacketSize);
                    return;
                }

                if (remainSize < packetSize)
                    break;

                base.OnReceived(m_buffer, readOffset, packetSize);

                remainSize -= packetSize;
                readOffset += packetSize;
            }

            if (0 < readOffset)
            {
                if (readOffset < m_bufferPos)
                {
                    Array.Copy(m_buffer, readOffset, m_buffer, 0, m_bufferPos - readOffset);
                    m_bufferPos -= readOffset;
                }
                else
                {
                    m_bufferPos = 0;
                }
            }
        }
    }
}
