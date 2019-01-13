using System.IO;

namespace fmCommon
{
    public class fmDataTableLoader
    {
        private fmDataTable m_tableFmData;

        public fmDataTable Load(string strPath)
        {
            m_tableFmData = new fmDataTable();
            if (false == LoadFile(strPath))
                return null;

            return m_tableFmData;
        }

        private bool LoadFile(string strPath)
        {
            using (BufferCoder coder = new BufferCoder())
            {
                using (FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read))
                {
                    long size = fs.Length;
                    byte[] output = coder.GetBuffer();
                    fs.Read(output, 0, output.Length);
                    fs.Close();
                }
                m_tableFmData.EncodeDecode(eCoderType.Decode, coder);
            }
            return Link();
        }

        private bool Link()
        {
            foreach (var nodes in fmLinker.m_dicLinks)
            {
                foreach (var node in nodes.Value)
                {
                    fmData fromFmData = m_tableFmData.Find(node.m_eFromFmDataType, node.m_nFromIndex);
                    if (null == fromFmData) { return false; }
                    fmData toFmData = m_tableFmData.Find(node.m_eToFmDataType, node.m_nToIndex);
                    if (null == toFmData) { return false; }

                    DataLink fromDataLink = DataLinkManager.Find(node.m_strFromKey, node.m_nFromIndex);
                    if (null != fromDataLink) { fromDataLink.Add(toFmData); }

                    DataLink toDataLink = DataLinkManager.Find(node.m_strToKey, node.m_nToIndex);
                    if (null != toDataLink) { toDataLink.Add(fromFmData); }
                }
            }
            return true;
        }
    }
}
