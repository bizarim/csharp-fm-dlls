using System;

namespace fmCommon
{
    /// <summary>
    /// Packet
    /// 일반적인 Byte 버퍼
    /// </summary>
    public class Packet : IDisposable
    {
        private bool m_disposed;
        // size
        public static readonly int BUFFER_SIZE = 10240;        // buffer size = 10240
        public static readonly int HEADER_SIZE = 8;            // header (8byte) = size (4byte) + type (4byte)

        // offset
        private static readonly int PACKET_SIZE_OFFSET = 0;      // 0 위치 읽는다.
        private static readonly int PACKET_TYPE_OFFSET = 4;      // 4 위치 읽는다.

        private eProtocolType m_eProtocolType = eProtocolType.PT_Unkwon;
        public void SeteProtocolType(eProtocolType type) { m_eProtocolType = type; }
        public eProtocolType GeteProtocolType() { return m_eProtocolType; }

        private byte[] m_buffer;                                // buffer
        public byte[] GetBuffer() { return m_buffer; }

        private int m_nBufferPos = 0;                           // buffer에 쓰는 위치
        //public int Position
        //{
        //    get { return m_nBufferPos; }
        //    set { m_nBufferPos = value; }
        //}

        private int m_nPacketLen = 0;
        public int GetPacketLen()
        {
            return m_nPacketLen;
        }

        //public static implicit operator Packet(fmProtocol fmp)
        //{
        //    return new Packet(fmp);
        //}
        public Packet GetOriginal()
        {
            return new Packet(m_buffer, m_nBufferPos, m_nPacketLen - m_nBufferPos);
        }

        public byte[] GetContents()
        {
            int length = m_nPacketLen - HEADER_SIZE;
            byte[] outPut = new byte[length];
            Array.Copy(m_buffer, HEADER_SIZE, outPut, 0, outPut.Length);
            return outPut;
        }

        private void ReCalcPacketSize()
        {
            BitConverter.GetBytes(m_nPacketLen).CopyTo(m_buffer, PACKET_SIZE_OFFSET);
        }

        public Packet()
        {
            m_nBufferPos = 0;
            m_nPacketLen = 0;
            m_buffer = new byte[BUFFER_SIZE];
            WriteInt(HEADER_SIZE);
        }

        public Packet(eProtocolType type)
        {
            m_nBufferPos = 0;
            m_nPacketLen = 0;
            m_buffer = new byte[BUFFER_SIZE];
            WriteInt(HEADER_SIZE);
            WriteInt((int)type);
            SeteProtocolType(type);
        }

        public Packet(fmProtocol _fmProtocol)
        {
            m_nBufferPos = 0;
            m_nPacketLen = 0;
            m_buffer = new byte[BUFFER_SIZE];
            WriteInt(HEADER_SIZE);
            _fmProtocol.Serialize(this);
        }

        public Packet(Packet p)
        {
            m_nBufferPos = 0;
            m_nPacketLen = 0;
            m_buffer = new byte[BUFFER_SIZE];
            WriteData(p.GetBuffer(), p.GetPacketLen());
        }

        public Packet(byte[] buffer, int STARTPOS, int len)
        {
            m_eProtocolType = (eProtocolType)BitConverter.ToInt32(buffer, PACKET_TYPE_OFFSET + STARTPOS);
            m_nBufferPos = 0;
            m_nPacketLen = 0;
            m_buffer = new byte[len];
            Array.Copy(buffer, STARTPOS, m_buffer, 0, len);
            m_nPacketLen = len;
        }

        public void Write(byte[] data)
        {
            data.CopyTo(m_buffer, m_nBufferPos);
            m_nBufferPos += data.Length;
            m_nPacketLen = m_nBufferPos;

            ReCalcPacketSize();
        }
        public void WriteByte(byte data)
        {
            const int len = 1;
            byte[] temp = new byte[len];
            temp[0] = data;

            Write(temp);
        }
        public void WriteBytes(byte[] data) { Write(data); }
        public void WriteUint(uint data) { Write(BitConverter.GetBytes(data)); }
        public void WriteUshort(ushort data) { Write(BitConverter.GetBytes(data)); }
        public void WriteDouble(double data) { Write(BitConverter.GetBytes(data)); }
        public void WriteFloat(float data) { Write(BitConverter.GetBytes(data)); }
        public void WriteChar(char data) { Write(BitConverter.GetBytes(data)); }
        public void WriteBool(bool data) { Write(BitConverter.GetBytes(data)); }
        public void WriteShort(short data) { Write(BitConverter.GetBytes(data)); }
        public void WriteLong(long data) { Write(BitConverter.GetBytes(data)); }
        public void WriteInt(int data) { Write(BitConverter.GetBytes(data)); }
        public void WriteString(string data)
        {
            //data = data.Trim();
            byte[] ConvData = System.Text.Encoding.Unicode.GetBytes(data);
            WriteInt(ConvData.Length);
            Write(ConvData);
        }

        public void WriteDateTime(DateTime data)
        {
            long d = data.ToBinary();
            Write(BitConverter.GetBytes(d));
        }

        public void WriteData(byte[] buffer, int len)
        {
            Array.Copy(buffer, 0, m_buffer, m_nBufferPos, len);
            m_nBufferPos += len;
            m_nPacketLen = m_nBufferPos;

            ReCalcPacketSize();
        }

        public void WritePacket(Packet p)
        {
            WriteData(p.GetBuffer(), p.GetPacketLen());
        }

        private void CheckValid(int size)
        {
            if (m_nBufferPos + size > m_nPacketLen)
                throw new ArgumentOutOfRangeException();
        }

        public uint ReadUint()
        {
            int size = sizeof(uint);
            CheckValid(size);

            uint read = BitConverter.ToUInt32(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public ushort ReadUshort()
        {
            int size = sizeof(ushort);
            CheckValid(size);

            ushort read = BitConverter.ToUInt16(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public double ReadDouble()
        {
            int size = sizeof(double);
            CheckValid(size);

            double read = BitConverter.ToDouble(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public float ReadFloat()
        {
            int size = sizeof(float);
            CheckValid(size);

            float read = BitConverter.ToSingle(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public byte ReadByte()
        {
            byte[] msg = new byte[1];
            Array.Copy(m_buffer, m_nBufferPos, msg, 0, 1);
            m_nBufferPos += sizeof(byte);
            return msg[0];
        }

        public char ReadChar()
        {
            int size = sizeof(char);
            CheckValid(size);

            char read = BitConverter.ToChar(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public bool ReadBool()
        {
            int size = sizeof(bool);
            CheckValid(size);

            bool read = BitConverter.ToBoolean(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public short ReadShort()
        {
            int size = sizeof(short);
            CheckValid(size);

            short read = BitConverter.ToInt16(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public long ReadLong()
        {
            int size = sizeof(long);
            CheckValid(size);

            long read = BitConverter.ToInt64(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public int ReadInt()
        {
            int size = sizeof(int);
            CheckValid(size);

            int read = BitConverter.ToInt32(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        public DateTime ReadDateTime()
        {
            long val = ReadLong();
            return DateTime.FromBinary(val);
        }

        public string ReadString()
        {
            int Count = ReadInt();
            string s = System.Text.Encoding.Unicode.GetString(m_buffer, m_nBufferPos, Count);
            m_nBufferPos += Count;
            return s;
        }

        public string ReadString(int Count)
        {
            string s = System.Text.Encoding.Unicode.GetString(m_buffer, m_nBufferPos, Count);
            m_nBufferPos += Count;
            string ts = s.TrimEnd('\0');
            return ts;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Packet()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                m_buffer = null;
            }

            m_disposed = true;
        }
    }
}
