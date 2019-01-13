using System;
using fmCommon;
using fmLibrary;
using StackExchange.Redis;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace fmServerCommon
{
    public class RedisMultiplexer : Singleton<RedisMultiplexer>
    {
        private Dictionary<eRedis, ConnectionMultiplexer> m_dicConns = new Dictionary<eRedis, ConnectionMultiplexer>();

        //public IDatabase GetDataBase(eDBRedis db)
        //{
        //    eRedis kind = (eRedis)Macro.HIWORD((int)db);
        //    int type = (int)db - Macro.LOWORD((int)kind);

        //    if (false == m_dicConns.ContainsKey(kind))
        //        return null;

        //    return m_dicConns[kind].GetDatabase(type);
        //}

        public IDatabase GetDataBase(eRedis db)
        {
            return m_dicConns[db].GetDatabase((int)db);
        }

        protected bool GetConnection(eRedis db, string strConn)
        {
            try
            {
                if (true == m_dicConns.ContainsKey(db))
                {
                    m_dicConns[db].Close();
                    m_dicConns.Remove(db);
                }
                ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(strConn);
                m_dicConns.Add(db, conn);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return false;
            }

            Logger.Info("Start ==> RedisMultiplexer {0}", db);

            return true;
        }

        public bool Start(eRedis db, string strConn = "localhost:6379")
        {
            if (true == m_dicConns.ContainsKey(db))
            {
                if (true == m_dicConns[db].IsConnected)
                {
                    Logger.Log("RedisMultiplexer Already Start");
                    return true;
                }
            }

            return GetConnection(db, strConn);
        }

        public void Stop(eRedis db)
        {
            if (true == m_dicConns.ContainsKey(db))
            {
                m_dicConns[db].Close();
                m_dicConns.Remove(db);
            }
        }

        //private bool TryGetDataBase(out IDatabase db,int index = 0)
        //{
        //    db = null;
        //    try
        //    {
        //        db = m_redisConnections.GetDatabase();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.ToString());
        //        Environment.Exit(1);
        //    }

        //    return true;
        //}

        //// set value
        //public void Set<T>(string key, T objValue) where T : class
        //{
        //    if (false == IsRun()) return;

        //    var db = m_redisConnections.GetDatabase();
        //    db.StringSet(string.Format("Token_{0}", key), new JavaScriptSerializer().Serialize(objValue));
        //}

        //// get value
        //public T Get<T>(string key) where T : class
        //{
        //    if (false == IsRun()) return default(T);

        //    var db = m_redisConnections.GetDatabase();
        //    var obj = db.StringGet(string.Format("Token_{0}", key));
        //    if (obj.HasValue)
        //        return new JavaScriptSerializer().Deserialize<T>(obj);
        //    else
        //        return default(T);
        //}

        //// Be Exists Key
        //public bool IsExistsKey(string key)
        //{
        //    if (false == IsRun()) return false;
        //    var db = m_redisConnections.GetDatabase();
        //    return db.KeyExists(key);
        //}

        //// Remove
        //public bool Remove(string key)
        //{
        //    if (false == IsRun()) return false;
        //    var db = m_redisConnections.GetDatabase();
        //    return db.KeyDelete(key);
        //}

        //public void Test()
        //{
        //    var db = m_redisConnections.GetDatabase();
        //    db.SortedSetAdd("","",1);
        //    //db.SortedSetScan
        //}
    }

}
