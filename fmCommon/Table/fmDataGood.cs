//namespace fmCommon
//{
//    public class fmDataGood : fmData
//    {
//        public int      m_nCode         = 0;
//        public int      m_nLv           = 0;
//        public eGrade   m_eGrade        = eGrade.Normal;
//        public eOption  m_eOption       = eOption.None;
//        public eParts   m_eParts        = eParts.None;
//        public int      m_nItemCode     = 0;
//        public int[]    m_nArrBaseOpts  = null;

//        public int[]    m_linkShopCode  = null;

//        public fmDataGood()
//        {
//            m_eFmDataType = eFmDataType.Good;
//        }

//        protected override int GetCode()
//        {
//            return m_nCode;
//        }

//        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
//        {
//            coder.EncodeDecode(eType, ref m_nCode);
//            coder.EncodeDecode(eType, ref m_nLv);
//            coder.EncodeDecode(eType, ref m_eGrade, sizeof(int));
//            coder.EncodeDecode(eType, ref m_eOption, sizeof(int));
//            coder.EncodeDecode(eType, ref m_eParts, sizeof(int));
//            coder.EncodeDecode(eType, ref m_nItemCode);
//            coder.EncodeDecode(eType, ref m_nArrBaseOpts);
//            coder.EncodeDecode(eType, ref m_linkShopCode);
//        }
//    }
//}
