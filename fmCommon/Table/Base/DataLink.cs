using System.Collections.Generic;
using System.Linq;

namespace fmCommon
{
    public class DataLinkManager : DataLink
    {
        private DataLinkManager() : base(string.Empty) { }

        public static DataLink Find(string strKey, int code)
        {
            if (strKey.Equals(string.Empty))
                return null;

            return m_dicDataLinks[strKey].Find(x => x.m_fromfmData.Code == code);
        }

        public static void Clear()
        {
            m_dicDataLinks.Clear();
        }
    }

    public class DataLink
    {
        protected static Dictionary<string, List<DataLink>> m_dicDataLinks = new Dictionary<string, List<DataLink>>();

        public fmData m_fromfmData = null;
        private string m_strLinkKey = string.Empty;

        protected DataLink(string strKey)
        {
            // DataLink는 생성과 동시에 Linker에 등록 된다.
            m_strLinkKey = strKey;
            Add();
        }

        public string GetKey() { return m_strLinkKey; }

        public virtual void Add(fmData data) { }
        public virtual int GetCount() { return 0; }

        private void Add()
        {
            if (null == m_dicDataLinks)
                m_dicDataLinks = new Dictionary<string, List<DataLink>>();

            bool isHave = m_dicDataLinks.ContainsKey(GetKey());
            if (false == isHave)
            {
                m_dicDataLinks.Add(GetKey(), new List<DataLink>());
            }

            m_dicDataLinks[GetKey()].Add(this);
        }
    }

    public class DataLinkDic : DataLink
    {
        Dictionary<int, fmData> m_dicDatas = new Dictionary<int, fmData>();

        public DataLinkDic(string strKey, fmData fromData)
            : base(strKey)
        {
            m_fromfmData = fromData;
        }

        public override int GetCount() { return m_dicDatas.Count; }

        public override void Add(fmData data)
        {
            m_dicDatas.Add(data.Code, data);
        }

        public fmData GetLinkFmData(int index)
        {
            if (m_dicDatas.ContainsKey(index))
                return m_dicDatas[index];

            return null;
        }

        public T GetFmData<T>() where T : fmData
        {
            if (0 == m_dicDatas.Count)
                return default(T);

            return m_dicDatas.ElementAt(0).Value as T;
        }

        public Dictionary<int, fmData> GetLinkDic()
        {
            return m_dicDatas;
        }
    }

    public class DataLinkList : DataLink
    {
        List<fmData> m_listDatas = new List<fmData>();

        public DataLinkList(string strKey, fmData fromData)
            : base(strKey)
        {
            m_fromfmData = fromData;
        }

        public override int GetCount() { return m_listDatas.Count; }

        public override void Add(fmData data)
        {
            fmData _data = GetFmData(data.Code);
            if (null == _data)
                m_listDatas.Add(data);
        }

        public fmData GetFmData(int code)
        {
            return m_listDatas.Find(x => x.Code == code);
        }

        public List<fmData> GetLinkList()
        {
            return m_listDatas;
        }
    }

    public class DataLinkOneZero : DataLink
    {
        fmData m_linkfmData = null;

        public DataLinkOneZero(string strKey, fmData fromData)
            : base(strKey)
        {
            m_fromfmData = fromData;
        }

        public override void Add(fmData data)
        {
            m_linkfmData = data;
        }

        public override int GetCount() { return 1; }

        public fmData GetLinkFmData()
        {
            return m_linkfmData;
        }
    }
}
