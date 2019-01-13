namespace fmCommon
{

    public class fmDataMonster : fmData
    {
        public int      m_nCode         = 0;
        public string   m_strNameCode   = string.Empty;
        public string   m_strImage      = string.Empty;

        public int      m_nLv           = 0;
        public int      m_nExtraLv      = 0;
        public eRareLv  m_eRareLv       = eRareLv.Bronze;
        public eTrait   m_eTrait        = eTrait.Normal;
        public eElement m_eElement      = eElement.None;
        public eUnit    m_eUnit         = eUnit.Monster;

        public fmDataMonster() { m_eFmDataType = eFmDataType.Monster; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_strNameCode);
            coder.EncodeDecode(eType, ref m_strImage);

            coder.EncodeDecode(eType, ref m_nLv);
            coder.EncodeDecode(eType, ref m_nExtraLv);
            coder.EncodeDecode(eType, ref m_eRareLv, sizeof(int));
            coder.EncodeDecode(eType, ref m_eTrait, sizeof(int));
            coder.EncodeDecode(eType, ref m_eElement, sizeof(int));
            coder.EncodeDecode(eType, ref m_eUnit, sizeof(int));
        }
    }
}
