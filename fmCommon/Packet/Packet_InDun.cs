using System.Collections.Generic;

namespace fmCommon
{
    //PT_CG_InDun_Refresh_RQ,
    //PT_CG_InDun_Refresh_RS,
    //PT_CG_InDun_Search_RQ,
    //PT_CG_InDun_Search_RS,
    //PT_CG_InDun_Clear_RQ,
    //PT_CG_InDun_Clear_RS,

    public class PT_CG_InDun_Search_RQ : fmPacket
    {
        public int m_nInDunCode = 0;
        public int m_nPlace = 0;

        public PT_CG_InDun_Search_RQ()
        {
            m_packetType = PacketType.PT_CG_InDun_Search_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nInDunCode);
            p.WriteInt(m_nPlace);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nInDunCode = p.ReadInt();
            m_nPlace = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nInDunCode = 0;
            m_nPlace = 0;
        }
    }

    public class PT_CG_InDun_Search_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<fmDiscovery> m_list = null;
        public rdLordInfo m_rdLordInfo = null;
        public rdStat m_rdStat = null;

        public PT_CG_InDun_Search_RS()
        {
            m_packetType = PacketType.PT_CG_InDun_Search_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;
            m_list.Write(ref p);
            m_rdLordInfo.Write(ref p);
            m_rdStat.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list = new List<fmDiscovery>();
            m_list.Read(ref p);

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);

            m_rdStat = new rdStat();
            m_rdStat.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_list = null;
            m_rdLordInfo = null;
            m_rdStat = null;
        }
    }

    public class PT_CG_InDun_Clear_RQ : fmPacket
    {
        public int Code { get; set; }

        public PT_CG_InDun_Clear_RQ()
        {
            m_packetType = PacketType.PT_CG_InDun_Clear_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(Code);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            Code = p.ReadInt();
        }

        protected override void Reset()
        {
            Code = 0;
        }
    }

    public class PT_CG_InDun_Clear_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public rdLordInfo m_rdLordInfo = null;
        public List<rdItem> m_items = null;
        public rdStat m_stat = null;
        public int m_nUnlockCode = 0;
        public rdInDun m_rdInDun = null;

        public fmDiscoveryRs m_discoveryRs = null;

        public PT_CG_InDun_Clear_RS()
        {
            m_packetType = PacketType.PT_CG_InDun_Clear_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            m_items.Write(ref p);
            m_stat.Write(ref p);

            m_discoveryRs.Write(ref p);
            p.WriteInt(m_nUnlockCode);
            m_rdInDun.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
            m_items = new List<rdItem>();
            m_items.Read(ref p);
            m_stat = new rdStat();
            m_stat.Read(ref p);
            m_discoveryRs = new fmDiscoveryRs();
            m_discoveryRs.Read(ref p);
            m_nUnlockCode = p.ReadInt();
            m_rdInDun = new rdInDun();
            m_rdInDun.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_items = null;
            m_stat = null;
            m_discoveryRs = null;
            m_nUnlockCode = 0;
            m_rdInDun = null;
        }
    }

    public class PT_CG_InDun_Refresh_RQ : fmPacket
    {
        public eRefresh m_eType = eRefresh.Ruby;
        public int m_nCode = 0;

        public PT_CG_InDun_Refresh_RQ()
        {
            m_packetType = PacketType.PT_CG_InDun_Refresh_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eType);
            p.WriteInt(m_nCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eType = (eRefresh)p.ReadInt();
            m_nCode = p.ReadInt();
        }

        protected override void Reset()
        {
            m_eType = eRefresh.Ruby;
            m_nCode = 0;
        }
    }

    public class PT_CG_InDun_Refresh_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;
        public rdInDun m_rdIndun = null;

        public PT_CG_InDun_Refresh_RS()
        {
            m_packetType = PacketType.PT_CG_InDun_Refresh_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            m_rdIndun.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);

            m_rdIndun = new rdInDun();
            m_rdIndun.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_rdIndun = null;
        }
    }

    public class PT_CG_InDun_OpenShortcut_RQ : fmPacket
    {
        public eRefresh m_eType = eRefresh.Ruby;
        public int m_nInDunCode = 0;


        public PT_CG_InDun_OpenShortcut_RQ()
        {
            m_packetType = PacketType.PT_CG_InDun_OpenShortcut_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eType);
            p.WriteInt(m_nInDunCode);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eType = (eRefresh)p.ReadInt();
            m_nInDunCode = p.ReadInt();
        }

        protected override void Reset()
        {
            m_eType = eRefresh.Ruby;
            m_nInDunCode = 0;
        }
    }

    public class PT_CG_InDun_OpenShortcut_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public rdLordInfo m_rdLordInfo = null;
        public rdInDun m_rdIndun = null;

        public PT_CG_InDun_OpenShortcut_RS()
        {
            m_packetType = PacketType.PT_CG_InDun_OpenShortcut_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            m_rdIndun.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);

            m_rdIndun = new rdInDun();
            m_rdIndun.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_rdIndun = null;
        }
    }
}
