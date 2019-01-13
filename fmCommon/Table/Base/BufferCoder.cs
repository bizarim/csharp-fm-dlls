using System;
using System.Text;

namespace fmCommon
{
    public enum eCoderType
    {
        Encode,
        Decode,
    }

    public partial class BufferCoder : IDisposable
    {
        // [ MUSTBE BY KWJ ] : 20150310
        // 암호화 체크썸 넣어야함.
        protected string m_strCheckSum = string.Empty;

        private static readonly int BUFFERSIZE = 10240 * 10;

        private byte[] m_buffer = new byte[BUFFERSIZE];
        private int m_nBufferPos = 0;

        public byte[] GetBuffer()
        {
            return m_buffer;
        }

        private int m_nBufferLen = 0;
        public int GetBufferLen()
        {
            return m_nBufferLen;
        }

        private void EncodeBytes(byte[] data) { Encode(data); }
        private void EncodeUint(uint data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeUshort(ushort data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeDouble(double data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeFloat(float data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeChar(char data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeBool(bool data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeShort(short data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeLong(long data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeInt(int data) { Encode(BitConverter.GetBytes(data)); }
        private void EncodeByte(byte data)
        {
            int len = 1;
            byte[] temp = new byte[len];
            temp[0] = data;

            Encode(temp);
        }

        private void EncodeString(string data)
        {
            byte[] ConvData = System.Text.Encoding.Unicode.GetBytes(data);
            EncodeInt(ConvData.Length);
            Encode(ConvData);
        }

        private void Encode(byte[] data)
        {
            data.CopyTo(m_buffer, m_nBufferPos);
            m_nBufferPos += data.Length;
            m_nBufferLen = m_nBufferPos;
        }

        private uint DecodeUint()
        {
            int size = sizeof(uint);
            uint read = BitConverter.ToUInt32(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private ushort DecodeUshort()
        {
            int size = sizeof(ushort);
            ushort read = BitConverter.ToUInt16(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private double DecodeDouble()
        {
            int size = sizeof(double);
            double read = BitConverter.ToDouble(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private float DecodeFloat()
        {
            int size = sizeof(float);
            float read = BitConverter.ToSingle(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private byte DecodeByte()
        {
            byte[] msg = new byte[1];
            Array.Copy(m_buffer, m_nBufferPos, msg, 0, 1);
            m_nBufferPos += sizeof(byte);
            return msg[0];
        }

        private char DecodeChar()
        {
            int size = sizeof(char);
            char read = BitConverter.ToChar(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private bool DecodeBool()
        {
            int size = sizeof(bool);
            bool read = BitConverter.ToBoolean(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private short DecodeShort()
        {
            int size = sizeof(short);
            short read = BitConverter.ToInt16(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private long DecodeLong()
        {
            int size = sizeof(long);
            long read = BitConverter.ToInt64(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private int DecodeInt()
        {
            int size = sizeof(int);
            int read = BitConverter.ToInt32(m_buffer, m_nBufferPos);
            m_nBufferPos += size;
            return read;
        }

        private string DecodeString()
        {
            int count = DecodeInt();
            string s = Encoding.Unicode.GetString(m_buffer, m_nBufferPos, count);
            m_nBufferPos += count;
            return s;
        }


        bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BufferCoder()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing) { }
            m_disposed = true;
        }
    }
}
