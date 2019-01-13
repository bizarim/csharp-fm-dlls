
using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Explore_GetList_RQ : fmPacket
    {
        public PT_CG_Explore_GetList_RQ()
        {
            m_packetType = PacketType.PT_CG_Explore_GetList_RQ;
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

    public class PT_CG_Explore_GetList_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdMap> m_maps = null;

        public PT_CG_Explore_GetList_RS()
        {
            m_packetType = PacketType.PT_CG_Explore_GetList_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_maps.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_maps = new List<rdMap>();
            m_maps.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

    public class PT_CG_Explore_Search_RQ : fmPacket
    {
        public int m_nCode = 0;
        public int m_nCount = 0;

        public PT_CG_Explore_Search_RQ()
        {
            m_packetType = PacketType.PT_CG_Explore_Search_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nCode);
            p.WriteInt(m_nCount);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nCode = p.ReadInt();
            m_nCount = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nCount = 0;
            m_nCount = 0;
        }
    }

    public class PT_CG_Explore_Search_RS : fmPacket
    {
        //protected virtual byte[] Compress(Packet p) { return null; }

        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<fmDiscovery> m_list = null;
        public rdStat m_rdStat = null;

        public PT_CG_Explore_Search_RS()
        {
            m_packetType = PacketType.PT_CG_Explore_Search_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            //Packet packet = new Packet(m_packetType);

            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);

                //bytes = null;
                return;
            }

            m_list.Write(ref p);
            m_rdStat.Write(ref p);
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);

                //bytes = null;
            }
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list = new List<fmDiscovery>();
            m_list.Read(ref p);

            m_rdStat = new rdStat();
            m_rdStat.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

    public class PT_CG_Explore_Clear_RQ : fmPacket
    {
        public int Code { get; set; }

        public PT_CG_Explore_Clear_RQ()
        {
            m_packetType = PacketType.PT_CG_Explore_Clear_RQ;
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

    public class PT_CG_Explore_Clear_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        //public int  m_nFindMap  = 0;
        //public bool m_bFind     = false;

        public rdLordInfo m_rdLordInfo = null;
        public List<rdItem> m_items = null;
        public rdStat m_stat = null;

        public fmDiscoveryRs m_discoveryRs = null;

        public PT_CG_Explore_Clear_RS()
        {
            m_packetType = PacketType.PT_CG_Explore_Clear_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            //p.WriteInt(m_nFindMap);
            //p.WriteBool(m_bFind);

            m_rdLordInfo.Write(ref p);
            m_items.Write(ref p);
            m_stat.Write(ref p);

            m_discoveryRs.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            //m_nFindMap = p.ReadInt();
            //m_bFind = p.ReadBool();

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
            m_items = new List<rdItem>();
            m_items.Read(ref p);
            m_stat = new rdStat();
            m_stat.Read(ref p);
            m_discoveryRs = new fmDiscoveryRs();
            m_discoveryRs.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            //m_nFindMap = 0;
            //m_bFind = false;
            m_rdLordInfo = null;
            m_items = null;
            m_stat = null;
            m_discoveryRs = null;
        }
    }

    public class PT_CG_Explore_NextLevel_NT : fmPacket
    {
        public eLevel Level { get; set; }

        public PT_CG_Explore_NextLevel_NT()
        {
            m_packetType = PacketType.PT_CG_Explore_NextLevel_NT;
            Level = eLevel.Normal;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)Level);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            Level = (eLevel)p.ReadInt();
        }

        protected override void Reset()
        {
        }
    }
}
