using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CG_Rank_GetList_RQ : fmProtocol
    {

        public PT_CG_Rank_GetList_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CG_Rank_GetList_RQ;
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

    public class PT_CG_Rank_GetList_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public long m_nMyRank = 0;
        public List<fmRanker> m_list = null;

        public PT_CG_Rank_GetList_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Rank_GetList_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            p.WriteLong(m_nMyRank);
            m_list.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_nMyRank = p.ReadLong();
            m_list = new List<fmRanker>();
            m_list.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_nMyRank = 0;
            m_list = null;
        }
    }

    //public class PT_CG_Rank_Nation_RQ : fmProtocol
    //{
    //    public int m_nPage = 0;

    //    public PT_CG_Rank_Nation_RQ()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Rank_Nation_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt(m_nPage);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_nPage = p.ReadInt();
    //    }

    //    protected override void Reset()
    //    {
    //        m_nPage = 0;
    //    }
    //}

    //public class PT_CG_Rank_Nation_RS : fmProtocol
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public int m_nMaxPage = 0;
    //    public List<rdRanker> m_list = null;

    //    public PT_CG_Rank_Nation_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Rank_Nation_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //    }
    //}
}
