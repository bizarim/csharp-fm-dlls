namespace fmCommon
{
    public class fmDataInDun : fmData
    {
        //nCode	nInDunCode	nPlace	nNextPlace	nLv	nAppearMoveMon	nAppearPlaceMon	nBoss	nGoblin	nForge	nArrDropOption
        public int      m_nCode             = 0;
        public int      m_nInDunCode        = 0;
        public int      m_nPlace            = 0;
        public int[]    m_nArrNextPlace     = null;
        public int      m_nLv               = 0;
        public int      m_nRound            = 0;
        public int[]    m_nAppearMon        = null;
        public int[]    m_nArrAppearRateMon = null;
        //public int[]    m_nAppearPlaceMon   = null;
        public int      m_nBoss             = 0;
        public int      m_nGoblin           = 0;
        public int      m_nForge            = 0;
        public int[]    m_nArrDropOption    = null;
        

        public float    m_xPos = 0;
        public float    m_yPos = 0;

        public fmDataInDun() { m_eFmDataType = eFmDataType.InDun; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode           );
            coder.EncodeDecode(eType, ref m_nInDunCode      );
            coder.EncodeDecode(eType, ref m_nPlace          );
            coder.EncodeDecode(eType, ref m_nArrNextPlace   );
            coder.EncodeDecode(eType, ref m_nLv             );
            coder.EncodeDecode(eType, ref m_nRound          );
            coder.EncodeDecode(eType, ref m_nAppearMon      );
            coder.EncodeDecode(eType, ref m_nArrAppearRateMon);
            coder.EncodeDecode(eType, ref m_nBoss           );
            coder.EncodeDecode(eType, ref m_nGoblin         );
            coder.EncodeDecode(eType, ref m_nForge          );
            coder.EncodeDecode(eType, ref m_nArrDropOption);

            coder.EncodeDecode(eType, ref m_xPos);
            coder.EncodeDecode(eType, ref m_yPos);
        }
    }
}
