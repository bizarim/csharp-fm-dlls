

using System.Collections.Generic;

namespace fmCommon
{
    public class fmLink
    {
        public string m_strToKey = string.Empty;
        public string m_strFromKey = string.Empty;

        public eFmDataType m_eToFmDataType;
        public eFmDataType m_eFromFmDataType;

        public int m_nToIndex;
        public int m_nFromIndex;
    }

    public class fmLinker
    {
        public static Dictionary<string, List<fmLink>> m_dicLinks = new Dictionary<string, List<fmLink>>();

        public static void Add(fmLink link)
        {
            if (null == m_dicLinks)
                m_dicLinks = new Dictionary<string, List<fmLink>>();

            if (false == link.m_strFromKey.Equals(string.Empty))
            {
                bool isHave = m_dicLinks.ContainsKey(link.m_strFromKey);
                if (false == isHave)
                    m_dicLinks.Add(link.m_strFromKey, new List<fmLink>());

                m_dicLinks[link.m_strFromKey].Add(link);
            }

            if (false == link.m_strToKey.Equals(string.Empty))
            {
                bool isHave = m_dicLinks.ContainsKey(link.m_strToKey);
                if (false == isHave)
                    m_dicLinks.Add(link.m_strToKey, new List<fmLink>());

                m_dicLinks[link.m_strToKey].Add(link);
            }
        }

        public static void Clear()
        {
            foreach (var node in m_dicLinks)
            {
                node.Value.Clear();
            }
            m_dicLinks.Clear();
        }
    }
}
