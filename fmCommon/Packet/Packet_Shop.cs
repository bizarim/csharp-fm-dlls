namespace fmCommon
{
    public class PT_CG_Shop_GetList_RQ : fmPacket
    {


        public PT_CG_Shop_GetList_RQ()
        {
            m_packetType = PacketType.PT_CG_Shop_GetList_RQ;
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

    public class PT_CG_Shop_GetList_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Shop_GetList_RS()
        {
            m_packetType = PacketType.PT_CG_Shop_GetList_RS;
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

    public class PT_CG_Shop_BuyGood_RQ : fmPacket
    {
        public int   m_nCode = 0;

        public PT_CG_Shop_BuyGood_RQ()
        {
            m_packetType = PacketType.PT_CG_Shop_BuyGood_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nCode = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nCode = 0;
        }
    }

    public class PT_CG_Shop_BuyGood_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;
        public rdItem m_item = null;
        public rdStat m_stats = null;

        public PT_CG_Shop_BuyGood_RS()
        {
            m_packetType = PacketType.PT_CG_Shop_BuyGood_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            m_item.Write(ref p);
            m_stats.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_item = new rdItem();
            m_stats = new rdStat();

            m_rdLordInfo.Read(ref p);
            m_item.Read(ref p);
            m_stats.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_item = null;
        }
    }


    public class PT_CG_Shop_BuyItemWithSeal_RQ : fmPacket
    {
        public eParts m_eParts = eParts.None;

        public PT_CG_Shop_BuyItemWithSeal_RQ()
        {
            m_packetType = PacketType.PT_CG_Shop_BuyItemWithSeal_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eParts);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eParts = (eParts)p.ReadInt();
        }

        protected override void Reset()
        {
            m_eParts = eParts.None;
        }
    }

    public class PT_CG_Shop_BuyItemWithSeal_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;
        public rdItem m_item = null;

        public PT_CG_Shop_BuyItemWithSeal_RS()
        {
            m_packetType = PacketType.PT_CG_Shop_BuyItemWithSeal_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            m_item.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_item = new rdItem();

            m_rdLordInfo.Read(ref p);
            m_item.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_item = null;
        }
    }
}
