using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Item_GetList_RQ : fmProtocol
    {
        public PT_CG_Item_GetList_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Item_GetList_RQ;
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

    public class PT_CG_Item_GetList_RS : fmProtocol
    {
        protected virtual byte[] Compress(Packet p) { return null; }

        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdItem> m_rdItems = null;

        public PT_CG_Item_GetList_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Item_GetList_RS;
        }


        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            Packet packet = new Packet(m_eProtocolType);

            packet.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
            {
                byte[] bytes = Compress(packet);
                if (null != bytes)
                    p.Write(bytes);
                bytes = null;
                return;
            }

            m_rdItems.Write(ref packet);

            {
                byte[] bytes = Compress(packet);
                if (null != bytes)
                    p.Write(bytes);
                bytes = null;
            }
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_rdItems = new List<rdItem>();
            m_rdItems.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItems = null;
        }
    }

    public class PT_CG_Item_Equip_RQ : fmProtocol
    {
        public int m_nSlot = 0;

        public PT_CG_Item_Equip_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Item_Equip_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nSlot);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nSlot = p.ReadInt();
        }

        protected override void Reset()
        {
        }
    }

    public class PT_CG_Item_Equip_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Item_Equip_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Item_Equip_RS;
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
