using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Lord_EnterWorld_RQ : fmProtocol
    {
        public string m_strToken = string.Empty;

        public PT_CG_Lord_EnterWorld_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_EnterWorld_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(m_strToken);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_strToken = p.ReadString();
        }

        protected override void Reset()
        {
            m_strToken = string.Empty;
        }
    }

    public class PT_CG_Lord_EnterWorld_RS : PT_LordInfo
    {
        //protected virtual byte[] Compress(Packet p) { return null; }

        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Lord_EnterWorld_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_EnterWorld_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            //Packet packet = new Packet(m_eProtocolType);

            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);
                //bytes = null;
                return;
            }

            m_rdLordInfo.Write(ref p);
            //m_rdItems.Write(ref p);
            m_rdStat.Write(ref p);
            m_rdMission.Write(ref p);
            m_rdMaps.Write(ref p);
            //m_prevEnchantList.Write(ref packet);
            m_missionBase.Write(ref p);
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);
                //bytes = null;
            }
            m_rdInDuns.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdStat = new rdStat();
            m_rdMission = new List<rdMission>();
            m_rdMaps = new List<rdMap>();
            m_missionBase = new rdMissionBase();
            m_rdInDuns = new List<rdInDun>();

            m_rdLordInfo.Read(ref p);
            m_rdStat.Read(ref p);
            m_rdMission.Read(ref p);
            m_rdMaps.Read(ref p);
            m_missionBase.Read(ref p);
            m_rdInDuns.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            m_rdStat = null;
        }
    }

    public class PT_CG_Lord_CreateLord_RQ : fmProtocol
    {
        public string m_strToken = string.Empty;
        public string m_strName = string.Empty;

        public PT_CG_Lord_CreateLord_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_CreateLord_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(m_strToken);
            p.WriteString(m_strName);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_strToken = p.ReadString();
            m_strName = p.ReadString();
        }

        protected override void Reset()
        {
            m_strToken = string.Empty;
            m_strName = string.Empty;
        }
    }

    public class PT_CG_Lord_CreateLord_RS : PT_LordInfo
    {
        //protected virtual byte[] Compress(Packet p) { return null; }

        public eErrorCode m_eErrorCode;

        public PT_CG_Lord_CreateLord_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_CreateLord_RS;
            m_eErrorCode = eErrorCode.Error;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            //Packet packet = new Packet(m_eProtocolType);

            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);
                //bytes = null;
                return;
            }

            m_rdLordInfo.Write(ref p);
            //m_rdItems.Write(ref p);
            m_rdStat.Write(ref p);
            m_rdMission.Write(ref p);
            m_rdMaps.Write(ref p);
            //m_prevEnchantList.Write(ref packet);
            m_missionBase.Write(ref p);
            {
                //byte[] bytes = Compress(packet);
                //if (null != bytes)
                //    p.Write(bytes);
                //bytes = null;
            }
            m_rdInDuns.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdStat = new rdStat();
            m_rdMission = new List<rdMission>();
            m_rdMaps = new List<rdMap>();
            m_missionBase = new rdMissionBase();
            m_rdInDuns = new List<rdInDun>();

            m_rdLordInfo.Read(ref p);
            m_rdStat.Read(ref p);
            m_rdMission.Read(ref p);
            m_rdMaps.Read(ref p);
            m_missionBase.Read(ref p);
            m_rdInDuns.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

    public class PT_CG_Lord_GetLord_RQ : fmProtocol
    {
        public string Name { get; set; }

        public PT_CG_Lord_GetLord_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_GetLord_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(Name);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            Name = p.ReadString();
        }

        protected override void Reset()
        {
            Name = string.Empty;
        }
    }

    public class PT_CG_Lord_GetLord_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public int Lv { get; set; }
        public List<rdItem> Items { get; set; }

        public PT_CG_Lord_GetLord_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Lord_GetLord_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            p.WriteInt(Lv);
            Items.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            Lv = p.ReadInt();

            if (null == Items)
                Items = new List<rdItem>();

            Items.Read(ref p);
        }

        protected override void Reset()
        {
            Lv = 0;
        }
    }

    public class PT_LordInfo : fmProtocol
    {
        public rdLordInfo m_rdLordInfo = null;
        public rdStat m_rdStat = null;
        public List<rdMission> m_rdMission = null;
        public List<rdMap> m_rdMaps = null;
        public rdMissionBase m_missionBase = null;
        public List<rdInDun> m_rdInDuns = null;

        protected override void Reset()
        {
            m_rdLordInfo = null;
            m_rdStat = null;
            m_rdMission = null;
            m_rdMaps = null;
            m_missionBase = null;
            m_rdInDuns = null;
        }
    }
}
