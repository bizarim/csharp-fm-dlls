using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace fmLibrary
{
    public interface IBufferManager
    {
        byte[] Take(int size);
        void Return(byte[] buffer);
    }

    // 버퍼 관리자 (GC) 안써. GC 호출하지마.
    public class GCBufferManager : IBufferManager
    {
        public byte[] Take(int size)
        {
            return new byte[size];
        }

        public void Return(byte[] buffer)
        {
        }
    }

    // 버퍼를 풀링하는 관리자
    // 여러사이즈의 버퍼풀을 관리하도록 구현
    public class PooledBufferManager : IBufferManager
    {
        private class PooledBuffer : IBufferManager
        {
            public int _allocSize;
            public int AllocCount { get { return _allocCount; } }
            private int _allocCount;
            private int _hitsCount;
            private int _missesCount;
            ConcurrentBag<byte[]> _buffers;

            public PooledBuffer(int size)
            {
                _allocCount = 0;
                _hitsCount = 0;
                _missesCount = 0;
                _buffers = new ConcurrentBag<byte[]>();

                _allocSize = size;
            }

            public byte[] Take(int size)
            {
                bool fill = false;
                byte[] buffer;
                while (!_buffers.TryTake(out buffer))
                {
                    fill = true;
                    FillBuffer();
                }
                if (fill)
                {
                    Interlocked.Increment(ref _missesCount);
                }
                else
                {
                    Interlocked.Increment(ref _hitsCount);
                }
                return buffer;
            }

            public void Return(byte[] buffer)
            {
                _buffers.Add(buffer);
            }

            private void FillBuffer()
            {
                try
                {
                    _buffers.Add(AllocNewBuffer(_allocSize));

                    Interlocked.Increment(ref _allocCount);
                }
                catch (Exception)
                {
                    //Logger.Error(string.Format("Alloc - size:{0},count:{1}", _allocSize, _allocCount));
                }
            }

            public string Dump()
            {
                return string.Format("alloc size:{0} - count:{1}, free:{2}, hit:{3}, miss:{4}", _allocSize, _allocCount, _buffers.Count, _hitsCount, _missesCount);
            }

            public static byte[] AllocNewBuffer(int size)
            {
                return new byte[size];
            }
        }

        // 버퍼 풀들
        private PooledBuffer[] _pools = null;

        // 생성자 
        // 사이즈에 해당하는 풀을 생성함
        public PooledBufferManager(int[] sizeArray)
        {
            Array.Sort(sizeArray);

            _pools = new PooledBuffer[sizeArray.Length];
            for (int i = 0; i < sizeArray.Length; ++i)
            {
                _pools[i] = new PooledBuffer(sizeArray[i]);
            }
        }

        // 메모리 획득하기
        // 획득할 메모리 사이즈, 만약 풀에 존재하지 않는 값이면 일반 Alloctor로 할당해서 넘김
        // 할당한 buffer
        public byte[] Take(int size)
        {
            PooledBuffer pooled = FindPool(size);
            if (pooled == null)
                return PooledBuffer.AllocNewBuffer(size);

            return pooled.Take(size);
        }

        // 메모리 반납하기
        // 반납할 buffer
        public void Return(byte[] buffer)
        {
            PooledBuffer pooled = FindPool(buffer.Length);
            if (pooled != null)
                pooled.Return(buffer);
        }
        // 현재 메모리풀들의 상태를 string으로 반환해서 넘긴다.
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var buffer in _pools)
            {
                builder.Append(buffer.Dump());
                builder.Append("\r\n");
            }

            return builder.ToString();
        }

        private PooledBuffer FindPool(int size)
        {
            foreach (var buffer in _pools)
            {
                if (size <= buffer._allocSize)
                    return buffer;
            }

            return null;
        }
    }
}
