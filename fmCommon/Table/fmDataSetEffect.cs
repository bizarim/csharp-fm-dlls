//namespace fmCommon
//{
//    public class fmDataSetEffect : fmData
//    {
//        public int      m_nCode     = 0;
//        public eOption  m_eSetOpt   = eOption.None;
//        public int      m_nSetCnt   = 0;
//        public eOption  m_eAddOpt   = eOption.None;
//        public float    m_fValue    = 0;

//        public fmDataSetEffect()
//        {
//            m_eFmDataType = eFmDataType.SetEffect;
//        }

//        protected override int GetCode()
//        {
//            return m_nCode;
//        }

//        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
//        {
//            coder.EncodeDecode(eType, ref m_nCode);
//            coder.EncodeDecode(eType, ref m_eSetOpt, sizeof(int));
//            coder.EncodeDecode(eType, ref m_nSetCnt);
//            coder.EncodeDecode(eType, ref m_eAddOpt, sizeof(int));
//            coder.EncodeDecode(eType, ref m_fValue);
//        }
//    }
//}
