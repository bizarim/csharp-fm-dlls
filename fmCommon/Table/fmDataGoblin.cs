//namespace fmCommon
//{

//    public class fmDataGoblin : fmData
//    {
//        public int m_nCode = 0;
//        public int[] m_nArrGoblin = null;
//        public int[] m_nArrDropKind = null;
//        public int[] m_nArrDropRate = null;

//        public fmDataGoblin() { m_eFmDataType = eFmDataType.Goblin; }
//        protected override int GetCode() { return m_nCode; }
//        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
//        {
//            coder.EncodeDecode(eType, ref m_nCode);
//            coder.EncodeDecode(eType, ref m_nArrGoblin);
//            coder.EncodeDecode(eType, ref m_nArrDropKind);
//            coder.EncodeDecode(eType, ref m_nArrDropRate);
//        }
//    }
//}
