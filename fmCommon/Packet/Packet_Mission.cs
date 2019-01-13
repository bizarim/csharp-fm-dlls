using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Mission_Refresh_RQ : fmPacket
    {
        public eRefresh m_eType = eRefresh.Ruby;

        public PT_CG_Mission_Refresh_RQ()
        {
            m_packetType = PacketType.PT_CG_Mission_Refresh_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eType);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eType = (eRefresh)p.ReadInt();
        }

        protected override void Reset()
        {
            m_eType = eRefresh.Ruby;
        }
    }

    public class PT_CG_Mission_Refresh_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdMission> m_list = null;
        public rdLordInfo m_rdLordInfo = null;
        public rdMissionBase m_missionBase = null;

        public PT_CG_Mission_Refresh_RS()
        {
            m_packetType = PacketType.PT_CG_Mission_Refresh_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list.Write(ref p);
            m_rdLordInfo.Write(ref p);
            m_missionBase.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list = new List<rdMission>();
            m_list.Read(ref p);
            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
            m_missionBase = new rdMissionBase();
            m_missionBase.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_list = null;
            m_rdLordInfo = null;
            m_missionBase = null;
        }
    }

    public class PT_CG_Mission_GetDailyList_RQ : fmPacket
    {
        public PT_CG_Mission_GetDailyList_RQ()
        {
            m_packetType = PacketType.PT_CG_Mission_GetDailyList_RQ;
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

    public class PT_CG_Mission_GetDailyList_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdMission>  m_list = null;
        public rdMissionBase    m_missionBase = null;

        public PT_CG_Mission_GetDailyList_RS()
        {
            m_packetType = PacketType.PT_CG_Mission_GetDailyList_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list.Write(ref p);
            m_missionBase.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list = new List<rdMission>();
            m_list.Read(ref p);
            m_missionBase = new rdMissionBase();
            m_missionBase.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_list = null;
            m_missionBase = null;
        }
    }

    public class PT_CG_Mission_DailyComplete_RQ : fmPacket
    {
        public int m_nCode = 0;

        public PT_CG_Mission_DailyComplete_RQ()
        {
            m_packetType = PacketType.PT_CG_Mission_DailyComplete_RQ;
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
        }
    }

    public class PT_CG_Mission_DailyComplete_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public rdLordInfo m_rdLordInfo = null;
        public bool IsLevelUp { get; set; }
        public rdStat m_stat = null;

        public PT_CG_Mission_DailyComplete_RS()
        {
            m_packetType = PacketType.PT_CG_Mission_DailyComplete_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo.Write(ref p);
            p.WriteBool(IsLevelUp);
            m_stat.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_rdLordInfo = new rdLordInfo();
            m_rdLordInfo.Read(ref p);
            IsLevelUp = p.ReadBool();

            m_stat = new rdStat();
            m_stat.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_rdLordInfo = null;
            IsLevelUp = false;
            m_stat = null;
        }
    }

    public class PT_CG_Mission_DailyCounting_RQ : fmPacket
    {
        //public eMission m_eMission = eMission.None;
        public int m_nCode = 0;
        public int m_nCondition = 0;

        public PT_CG_Mission_DailyCounting_RQ()
        {
            m_packetType = PacketType.PT_CG_Mission_DailyCounting_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nCode);
            p.WriteInt(m_nCondition);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nCode = p.ReadInt();
            m_nCondition = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nCode = 0;
            m_nCondition = 0;
        }
    }

    public class PT_CG_Mission_DailyCounting_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Mission_DailyCounting_RS()
        {
            m_packetType = PacketType.PT_CG_Mission_DailyCounting_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }
}
