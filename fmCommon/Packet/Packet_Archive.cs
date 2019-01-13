
using System.Collections.Generic;

namespace fmCommon
{
    //public class PT_CG_Archive_GetWall_RQ : fmPacket
    //{
    //    public string m_strName = string.Empty;

    //    public PT_CG_Archive_GetWall_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_GetWall_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteString(m_strName);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_strName = p.ReadString();
    //    }

    //    protected override void Reset()
    //    {
    //        m_strName = string.Empty;
    //    }
    //}

    //public class PT_CG_Archive_GetWall_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public List<rdWall> m_wall = null;

    //    public PT_CG_Archive_GetWall_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_GetWall_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall = new List<rdWall>();
    //        m_wall.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_wall = null;
    //    }
    //}

    //public class PT_CG_Archive_WriteWall_RQ : fmPacket
    //{
    //    public string m_strName = string.Empty;
    //    public string m_strContents = string.Empty;

    //    public PT_CG_Archive_WriteWall_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_WriteWall_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteString(m_strName);
    //        p.WriteString(m_strContents);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_strName = p.ReadString();
    //        m_strContents = p.ReadString();
    //    }

    //    protected override void Reset()
    //    {
    //        m_strName = string.Empty;
    //        m_strContents = string.Empty;
    //    }
    //}

    //public class PT_CG_Archive_WriteWall_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public rdWall m_wall = null;

    //    public PT_CG_Archive_WriteWall_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_WriteWall_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall = new rdWall();
    //        m_wall.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_wall = null;
    //    }
    //}

    //public class PT_CG_Archive_GetNationWall_RQ : fmPacket
    //{
    //    public PT_CG_Archive_GetNationWall_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_GetNationWall_RQ;
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
    //public class PT_CG_Archive_GetNationWall_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public List<rdWall> m_wall = null;

    //    public PT_CG_Archive_GetNationWall_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_GetNationWall_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall = new List<rdWall>();
    //        m_wall.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_wall = null;
    //    }
    //}

    //public class PT_CG_Archive_WriteNationWall_RQ : fmPacket
    //{
    //    public string m_strContents = string.Empty;

    //    public PT_CG_Archive_WriteNationWall_RQ()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_WriteNationWall_RQ;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteString(m_strContents);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_strContents = p.ReadString();
    //    }

    //    protected override void Reset()
    //    {
    //        m_strContents = string.Empty;
    //    }
    //}
    //public class PT_CG_Archive_WriteNationWall_RS : fmPacket
    //{
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;
    //    public rdWall m_wall = null;

    //    public PT_CG_Archive_WriteNationWall_RS()
    //    {
    //        m_packetType = PacketType.PT_CG_Archive_WriteNationWall_RS;
    //    }

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall.Write(ref p);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //        if (m_eErrorCode != eErrorCode.Success)
    //            return;

    //        m_wall = new rdWall();
    //        m_wall.Read(ref p);
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //        m_wall = null;
    //    }
    //}

    public class PT_CG_Archive_GetMailList_RQ : fmPacket
    {
        public int m_nPageCnt = 0;

        public PT_CG_Archive_GetMailList_RQ()
        {
            m_packetType = PacketType.PT_CG_Archive_GetMailList_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nPageCnt);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nPageCnt = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nPageCnt = 0;
        }
    }

    public class PT_CG_Archive_GetMailList_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<rdMail> m_mails = null;
        public int m_nMaxPage = 0;

        public PT_CG_Archive_GetMailList_RS()
        {
            m_packetType = PacketType.PT_CG_Archive_GetMailList_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_mails.Write(ref p);
            p.WriteInt(m_nMaxPage);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (m_eErrorCode != eErrorCode.Success)
                return;
            m_mails = new List<rdMail>();
            m_mails.Read(ref p);
            m_nMaxPage = p.ReadInt();
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_mails = null;
            m_nMaxPage = 0;
        }
    }

    public class PT_CG_Archive_TakeGood_RQ : fmPacket
    {
        public PT_CG_Archive_TakeGood_RQ()
        {
            m_packetType = PacketType.PT_CG_Archive_TakeGood_RQ;
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

    public class PT_CG_Archive_TakeGood_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_Archive_TakeGood_RS()
        {
            m_packetType = PacketType.PT_CG_Archive_TakeGood_RS;
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
