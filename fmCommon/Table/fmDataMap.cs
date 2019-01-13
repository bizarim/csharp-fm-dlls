namespace fmCommon
{
    /// <summary>
    /// 아이템 base
    /// </summary>
    public class fmDataMap : fmData
    {
        public int m_nCode = 0;
        public int m_nMap = 0;
        public string   m_strNameCode       = string.Empty;

        public fmDataMap() { m_eFmDataType = eFmDataType.Map; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_nMap);
            coder.EncodeDecode(eType, ref m_strNameCode);
        }
    }
}
