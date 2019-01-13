namespace fmCommon
{
    public class fmDataShop : fmData
    {
        public int      m_nCode         = 0;
        public eShop    m_eShop         = eShop.Google;
        public string   m_strName       = string.Empty;

        public eReward  m_eReward       = eReward.None;
        public int      m_nAmount       = 0;

        public eFinance m_eFinance      = eFinance.None;
        public float    m_fNeed         = 0;

        public string   m_strCashCode   = string.Empty;

        public eGrade   m_eAddItemGrade = eGrade.None;

        //public int m_nGood = 0;
        //public DataLinkOneZero m_linkGood = null;

        public fmDataShop()
        {
            m_eFmDataType = eFmDataType.Shop;
            //m_linkGood = new DataLinkOneZero("m_linkGood", this);
        }

        protected override int GetCode()
        {
            return m_nCode;
        }

        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_eShop, sizeof(int));
            coder.EncodeDecode(eType, ref m_strName);

            coder.EncodeDecode(eType, ref m_eReward, sizeof(int));
            coder.EncodeDecode(eType, ref m_nAmount);

            coder.EncodeDecode(eType, ref m_eFinance, sizeof(int));
            coder.EncodeDecode(eType, ref m_fNeed);

            coder.EncodeDecode(eType, ref m_strCashCode);

            coder.EncodeDecode(eType, ref m_eAddItemGrade, sizeof(int));
        }
    }
}
