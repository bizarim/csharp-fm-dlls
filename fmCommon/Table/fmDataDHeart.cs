//namespace fmCommon
//{
//    public class fmDataDHeart : fmData
//    {
//        public int m_nCode = 0;
//        public string m_strNameCode = string.Empty;
//        public string m_strImage = string.Empty;

//        public eLevel m_eLevel = eLevel.Normal;
//        public int m_nEnterLv = 0;

//        public int m_nDropCnt = 0;

//        public int[] m_nArrAppearDragon = null;
//        public int[] m_nArrAppearRateDragon = null;

//        public int[] m_nArrAppearMon = null;
//        public int[] m_nArrAppearRateMon = null;


//        public fmDataDHeart() { m_eFmDataType = eFmDataType.DHeart; }
//        protected override int GetCode() { return m_nCode; }
//        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
//        {
//            coder.EncodeDecode(eType, ref m_nCode);
//            coder.EncodeDecode(eType, ref m_strNameCode);
//            coder.EncodeDecode(eType, ref m_strImage);
//            coder.EncodeDecode(eType, ref m_eLevel, sizeof(int));
//            coder.EncodeDecode(eType, ref m_nEnterLv);
//            coder.EncodeDecode(eType, ref m_nDropCnt);
//            coder.EncodeDecode(eType, ref m_nArrAppearDragon);
//            coder.EncodeDecode(eType, ref m_nArrAppearRateDragon);
//            coder.EncodeDecode(eType, ref m_nArrAppearMon);
//            coder.EncodeDecode(eType, ref m_nArrAppearRateMon);
//        }
//    }
//}
