namespace fmCommon
{
    public class PT_CG_Shop_GetList_RQ : fmProtocol
    {


        public PT_CG_Shop_GetList_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Shop_GetList_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
        }

        protected override void Reset()
        {
        }
    }

    public class PT_CG_Shop_GetList_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Shop_GetList_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Shop_GetList_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

}
