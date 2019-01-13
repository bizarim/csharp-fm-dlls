namespace fmCommon
{
    public class fmDataExplore : fmData
    {
        public int      m_nCode             = 0;
        public int      m_nMapCode          = 0;
        public int      m_nInDun            = 0;
        public int      m_nLinkCode         = 0;
        public int      m_nNextCode         = 0;
        public float    m_xPos              = 0;
        public float    m_yPos              = 0;
        public int      m_nEnterLv          = 0;
        public int[]    m_nArrAppearMon     = null;
        public int[]    m_nArrAppearRateMon = null;

        public fmDataExplore() { m_eFmDataType = eFmDataType.Explore; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_nMapCode);
            coder.EncodeDecode(eType, ref m_nInDun);
            coder.EncodeDecode(eType, ref m_nLinkCode);
            coder.EncodeDecode(eType, ref m_nNextCode);
            coder.EncodeDecode(eType, ref m_xPos);
            coder.EncodeDecode(eType, ref m_yPos);
            coder.EncodeDecode(eType, ref m_nEnterLv);
            coder.EncodeDecode(eType, ref m_nArrAppearMon);
            coder.EncodeDecode(eType, ref m_nArrAppearRateMon);
        }
    }
}
