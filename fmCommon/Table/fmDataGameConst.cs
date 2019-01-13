//namespace fmCommon
//{
//    public class fmDataGameConst : fmData
//    {
//        public int m_nCode          = 0;
//        public string m_strKey      = string.Empty;
//        public long m_biValue       = 0;

//        public fmDataGameConst()
//        {
//            m_eFmDataType = eFmDataType.GameConst;
//        }
//        protected override int GetCode() { return m_nCode; }
//        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
//        {
//            coder.EncodeDecode(eType, ref m_nCode);
//            coder.EncodeDecode(eType, ref m_strKey);
//            coder.EncodeDecode(eType, ref m_biValue);
//        }
//    }
//}
