namespace fmCommon
{
    public class fmDataDropValue : fmData
    {
        public int      m_nCode         = 0;
        public string   m_strDropPlace  = string.Empty;
        public eGrade   m_eGrade        = eGrade.Normal;
        public eParts   m_eParts        = eParts.None;
        public int      m_nLimitValue   = 0;
        public int      m_nRate         = 0;


        public fmDataDropValue() { m_eFmDataType = eFmDataType.DropValue; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_strDropPlace);
            coder.EncodeDecode(eType, ref m_eGrade, sizeof(int));
            coder.EncodeDecode(eType, ref m_eParts, sizeof(int));
            coder.EncodeDecode(eType, ref m_nLimitValue);
            coder.EncodeDecode(eType, ref m_nRate);
        }
    }
}
