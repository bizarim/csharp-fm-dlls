
namespace fmCommon
{
    /// <summary>
    /// 아이템 base
    /// </summary>
    public class fmDataItem : fmData
    {
        public int      m_nCode             = 0;
        public string   m_strNameCode       = string.Empty;
        public string   m_strImage          = string.Empty;
        public eBeyond  m_eBeyond           = eBeyond.None;
        public eParts   m_eParts            = eParts.None;
        public int      m_nPrice            = 0;
        public eWeapon  m_eWeapon           = eWeapon.None;
        public int[]    m_nArrOptions       = null;

        public fmDataItem() { m_eFmDataType = eFmDataType.Item; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_strNameCode);
            coder.EncodeDecode(eType, ref m_strImage);
            coder.EncodeDecode(eType, ref m_eBeyond, sizeof(int));
            coder.EncodeDecode(eType, ref m_eParts, sizeof(int));
            coder.EncodeDecode(eType, ref m_nPrice);
            coder.EncodeDecode(eType, ref m_eWeapon, sizeof(int));
            coder.EncodeDecode(eType, ref m_nArrOptions);
        }
    }
}
