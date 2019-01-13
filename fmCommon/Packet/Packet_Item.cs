using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Item_GetList_RQ : fmPacket
    {
        public PT_CG_Item_GetList_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_GetList_RQ;
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

    public class PT_CG_Item_GetList_RS : fmPacket
    {
        protected virtual byte[] Compress(Packet p) { return null; }

        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdItem> m_rdItems = null;

        public PT_CG_Item_GetList_RS()
        {
            m_packetType = PacketType.PT_CG_Item_GetList_RS;
        }


        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            Packet packet = new Packet(m_packetType);

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

    public class PT_CG_Item_Equip_RQ : fmPacket
    {
        public int m_nSlot = 0;

        public PT_CG_Item_Equip_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_Equip_RQ;
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

    public class PT_CG_Item_Equip_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Item_Equip_RS()
        {
            m_packetType = PacketType.PT_CG_Item_Equip_RS;
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

    public class PT_CG_Item_Sell_RQ : fmPacket
    {
        public List<int> m_slots = new List<int>();

        public PT_CG_Item_Sell_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_Sell_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            m_slots.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_slots = new List<int>();
            m_slots.Read(ref p);
        }

        protected override void Reset()
        {
        }
    }

    public class PT_CG_Item_Sell_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_Sell_RS()
        {
            m_packetType = PacketType.PT_CG_Item_Sell_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
        }
    }

    //public class PT_CG_Item_Reroll_RQ : fmPacket
    //{
    //    public eRefresh m_type = eRefresh.Gold;

    //    public int m_nSlot = 0;

    //    public PT_CG_Item_Reroll_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_Reroll_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt(m_nSlot);
    //        p.WriteInt((int)m_type);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_nSlot = p.ReadInt();
    //        m_type = (eRefresh)p.ReadInt();
    //    }

    //    protected override void Reset()
    //    {
    //        m_nSlot = 0;
    //        m_type = eRefresh.Gold;
    //    }
    //}

    //public class PT_CG_Item_Reroll_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public rdItem m_rdItem = null;
    //    public rdLordInfo m_rdLordInfo = null;
    //    public bool IsSuccess { get; set; }

    //    public PT_CG_Item_Reroll_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_Reroll_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_rdItem.Write(ref p);
    //        m_rdLordInfo.Write(ref p);
    //        p.WriteBool(IsSuccess);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_rdItem = new rdItem();
    //        m_rdItem.Read(ref p);
    //        m_rdLordInfo = new rdLordInfo();
    //        m_rdLordInfo.Read(ref p);
    //        IsSuccess = p.ReadBool();
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_rdItem = null;
    //        m_rdLordInfo = null;
    //        IsSuccess = false;
    //    }
    //}

    public class PT_CG_Item_Remelt_RQ : fmPacket
    {
        public int m_nSlot = 0;
        public int m_nOptIndex = 0;
        public eOption m_selectOpt = eOption.None;

        public PT_CG_Item_Remelt_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_Remelt_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nSlot);
            p.WriteInt(m_nOptIndex);
            p.WriteInt((int)m_selectOpt);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nSlot = p.ReadInt();
            m_nOptIndex = p.ReadInt();
            m_selectOpt = (eOption)p.ReadInt();
        }

        protected override void Reset()
        {
            m_nSlot = 0;
            m_nOptIndex = 0;
            m_selectOpt = eOption.None;
        }
    }

    public class PT_CG_Item_Remelt_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdItem m_rdItem = null;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_Remelt_RS()
        {
            m_packetType = PacketType.PT_CG_Item_Remelt_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem.Write(ref p);
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem = new rdItem();
            m_rdItem.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItem = null;
            m_rdLordInfo = null;
        }
    }

    public class PT_CG_Item_UpToAncient_RQ : fmPacket
    {
        public int m_nSlot = 0;
        public int m_nInDunCode = 0;

        public PT_CG_Item_UpToAncient_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_UpToAncient_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nSlot);
            p.WriteInt(m_nInDunCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nSlot = p.ReadInt();
            m_nInDunCode = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nSlot = 0;
            m_nInDunCode = 0;
        }
    }

    public class PT_CG_Item_UpToAncient_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdItem m_rdItem = null;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_UpToAncient_RS()
        {
            m_packetType = PacketType.PT_CG_Item_UpToAncient_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem.Write(ref p);
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem = new rdItem();
            m_rdItem.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItem = null;
            m_rdLordInfo = null;
        }
    }

    //public class PT_CG_Item_PrevEnchantList_RQ : fmPacket
    //{
    //    public PT_CG_Item_PrevEnchantList_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_PrevEnchantList_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //    }

    //    protected override void Reset()
    //    {
    //    }
    //}

    //public class PT_CG_Item_PrevEnchantList_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;

    //    public rdEnchantList m_list = null;

    //    public PT_CG_Item_PrevEnchantList_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_PrevEnchantList_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_list.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_list = new rdEnchantList();
    //        m_list.Read(ref p);

    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_list = null;
    //    }
    //}

    //public class PT_CG_Item_GetEnchantList_RQ : fmPacket
    //{
    //    public int m_nItemSlot = 0;
    //    public eFinance m_eFinance = eFinance.None;

    //    public PT_CG_Item_GetEnchantList_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_GetEnchantList_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt(m_nItemSlot);
    //        p.WriteInt((int)m_eFinance);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_nItemSlot = p.ReadInt();
    //        m_eFinance = (eFinance)p.ReadInt();
    //    }

    //    protected override void Reset()
    //    {
    //        m_nItemSlot = 0;
    //        m_eFinance = eFinance.None;
    //    }
    //}

    //public class PT_CG_Item_GetEnchantList_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;

    //    public rdEnchantList m_list = null;

    //    public PT_CG_Item_GetEnchantList_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_GetEnchantList_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_list.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_list = new rdEnchantList();
    //        m_list.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_list = null;
    //    }
    //}

    //public class PT_CG_Item_Enchant_RQ : fmPacket
    //{
    //    public int m_nItemSlot = 0;
    //    public eOption m_select = eOption.None;
    //    public eFinance m_eFinance = eFinance.None;

    //    public PT_CG_Item_Enchant_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_Enchant_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt(m_nItemSlot);
    //        p.WriteInt((int)m_select);
    //        p.WriteInt((int)m_eFinance);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_nItemSlot = p.ReadInt();
    //        m_select = (eOption)p.ReadInt();
    //        m_eFinance = (eFinance)p.ReadInt();
    //    }

    //    protected override void Reset()
    //    {
    //        m_nItemSlot = 0;
    //        m_select = eOption.None;
    //        m_eFinance = eFinance.None;
    //    }
    //}

    //public class PT_CG_Item_Enchant_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public rdItem m_rdItem = null;
    //    public rdLordInfo m_rdLordInfo = null;

    //    public PT_CG_Item_Enchant_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Item_Enchant_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_rdItem.Write(ref p);
    //        m_rdLordInfo.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_rdItem = new rdItem();
    //        m_rdItem.Read(ref p);
    //        m_rdLordInfo = new rdLordInfo();
    //        m_rdLordInfo.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_rdItem = null;
    //        m_rdLordInfo = null;
    //    }
    //}

    public class PT_CG_Item_CreateMythic_RQ : fmPacket
    {
        public int m_nItemSlot = 0;
        public eOption m_selectOpt = eOption.None;
        public int m_nInDunCode = 0;
        public eElement m_element = eElement.None;

        public PT_CG_Item_CreateMythic_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_CreateMythic_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nItemSlot);
            p.WriteInt((int)m_selectOpt);
            p.WriteInt(m_nInDunCode);
            p.WriteInt((int)m_element);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nItemSlot = p.ReadInt();
            m_selectOpt = (eOption)p.ReadInt();
            m_nInDunCode = p.ReadInt();
            m_element = (eElement)p.ReadInt();
        }

        protected override void Reset()
        {
            m_nItemSlot = 0;
            m_selectOpt = eOption.None;
            m_nInDunCode = 0;
            m_element = eElement.None;
        }
    }

    public class PT_CG_Item_CreateMythic_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdItem m_rdItem = null;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_CreateMythic_RS()
        {
            m_packetType = PacketType.PT_CG_Item_CreateMythic_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem.Write(ref p);
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem = new rdItem();
            m_rdItem.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItem = null;
            m_rdLordInfo = null;
        }
    }

    public class PT_CG_Item_AddOptToMythic_RQ : fmPacket
    {
        public int m_nItemSlot = 0;
        public eOption m_selectOpt = eOption.None;
        public int m_nInDunCode = 0;

        public PT_CG_Item_AddOptToMythic_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_AddOptToMythic_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nItemSlot);
            p.WriteInt((int)m_selectOpt);
            p.WriteInt(m_nInDunCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nItemSlot = p.ReadInt();
            m_selectOpt = (eOption)p.ReadInt();
            m_nInDunCode = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nItemSlot = 0;
            m_selectOpt = eOption.None;
            m_nInDunCode = 0;
        }
    }

    public class PT_CG_Item_AddOptToMythic_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdItem m_rdItem = null;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_AddOptToMythic_RS()
        {
            m_packetType = PacketType.PT_CG_Item_AddOptToMythic_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem.Write(ref p);
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem = new rdItem();
            m_rdItem.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItem = null;
            m_rdLordInfo = null;
        }
    }

    public class PT_CG_Item_UseCombineCube_RQ : fmPacket
    {
        public int m_nBaseItemSlot = 0;
        public int m_nOtherItemSlot = 0;

        public PT_CG_Item_UseCombineCube_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_UseCombineCube_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nBaseItemSlot);
            p.WriteInt(m_nOtherItemSlot);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nBaseItemSlot = p.ReadInt();
            m_nOtherItemSlot = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nBaseItemSlot = 0;
            m_nOtherItemSlot = 0;
        }
    }

    public class PT_CG_Item_UseCombineCube_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;
        public bool m_isSuccess = false;
        public rdItem m_rdItem = null;

        public PT_CG_Item_UseCombineCube_RS()
        {
            m_packetType = PacketType.PT_CG_Item_UseCombineCube_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            p.WriteBool(m_isSuccess);
            if (m_isSuccess)
                m_rdItem.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
            m_isSuccess = p.ReadBool();
            if (m_isSuccess)
            {
                m_rdItem = new rdItem();
                m_rdItem.Read(ref p);
            }
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_isSuccess = false;
            m_rdItem = null;
        }
    }

    public class PT_CG_Item_RemoveRemelt_RQ : fmPacket
    {
        public int m_nSlot = 0;

        public PT_CG_Item_RemoveRemelt_RQ()
        {
            m_packetType = PacketType.PT_CG_Item_RemoveRemelt_RQ;
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
            m_nSlot = 0;
        }
    }

    public class PT_CG_Item_RemoveRemelt_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdItem m_rdItem = null;
        public rdLordInfo m_rdLordInfo = null;

        public PT_CG_Item_RemoveRemelt_RS()
        {
            m_packetType = PacketType.PT_CG_Item_RemoveRemelt_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem.Write(ref p);
            m_rdLordInfo.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdItem = new rdItem();
            m_rdItem.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdItem = null;
            m_rdLordInfo = null;
        }
    }

}
