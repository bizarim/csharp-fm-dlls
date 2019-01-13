namespace fmCommon
{
    public class fmDataOption : fmData
    {
        public int      m_nCode         = 0;
        public eOption  m_eOption       = eOption.None;
        public int      m_nAppearLv     = 0;
        public int[]    m_nArrParts     = null;
        public int[]    m_nArrBeyond    = null;
        //public int      m_nWorld        = 0;
        //public int      m_nInDun        = 0;

        public fmDataOption() { m_eFmDataType = eFmDataType.Option; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_eOption, sizeof(int));
            coder.EncodeDecode(eType, ref m_nAppearLv);
            coder.EncodeDecode(eType, ref m_nArrParts);
            coder.EncodeDecode(eType, ref m_nArrBeyond);
            //coder.EncodeDecode(eType, ref m_nWorld);
            //coder.EncodeDecode(eType, ref m_nInDun);
        }
    }
}
