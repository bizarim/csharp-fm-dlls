using System;
using StackExchange.Redis;
using fmCommon;

namespace fmServerCommon
{
    public abstract class UserRedisQuery : IDisposable
    {
        protected eRedis m_eDataBase = eRedis.Token;
        public abstract eErrorCode Execute();
        public abstract void Release();

        protected IDatabase GetDatabase()
        {
            return RedisMultiplexer.Instance.GetDataBase(m_eDataBase);
        }

        //protected IDatabase GetDatabase(eDBRedis edb)
        //{
        //    return RedisMultiplexer.Instance.GetDataBase(edb);
        //}

        protected bool IsExistsName(string name)
        {
            return RedisMultiplexer.Instance.GetDataBase(eRedis.Game).IsExistsName(name);
        }

        protected bool InsertName(string name, long accid)
        {
            return RedisMultiplexer.Instance.GetDataBase(eRedis.Game).StringSet(name, accid);
        }

        protected long GetAccIdWithName(string name)
        {
            return RedisMultiplexer.Instance.GetDataBase(eRedis.Game).GetAccIdWithName(name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UserRedisQuery()
        {
            Dispose(false);
        }

        bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                Release();
            }

            m_disposed = true;
        }
    }
}
