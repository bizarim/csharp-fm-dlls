using System.Collections.Generic;

namespace fmCommon
{
    public class PT_CA_Auth_Login_RQ : fmPacket
    {
        protected virtual string Encrypt(string str) { return str; }
        protected virtual string Decrypt(string str) { return str; }

        public string   m_strUniqueKey  = string.Empty;
        public int      m_nAppos        = 0;
        public string   m_strCver       = string.Empty;
        public string   m_strSver       = string.Empty;

        public PT_CA_Auth_Login_RQ()
        {
            m_packetType = PacketType.PT_CA_Auth_Login_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(Encrypt(m_strUniqueKey));
            p.WriteInt(m_nAppos);
            p.WriteString(m_strCver);
            p.WriteString(m_strSver);
        }

        public override void Deserialize(Packet p)
        {
            
            base.Deserialize(p);
            m_strUniqueKey = Decrypt(p.ReadString());
            m_nAppos = p.ReadInt();
            m_strCver = p.ReadString();
            m_strSver = p.ReadString();
        }

        protected override void Reset()
        {
            m_strUniqueKey = string.Empty;
            m_nAppos = 0;
            m_strCver = string.Empty;
            m_strSver = string.Empty;
        }
    }

    //public class fmPacketResult : fmPacket
    //{
    //    public OnErrorHandler m_fnErr = null;
    //    public eErrorCode m_eErrorCode = eErrorCode.Error;

    //    public override void Serialize(Packet p)
    //    {
    //        base.Serialize(p);
    //        p.WriteInt((int)m_eErrorCode);
    //    }

    //    public override void Deserialize(Packet p)
    //    {
    //        base.Deserialize(p);
    //        m_eErrorCode = (eErrorCode)p.ReadInt();
    //    }

    //    protected override void Reset()
    //    {
    //        m_eErrorCode = eErrorCode.Error;
    //    }

    //    public bool TryParse(Packet packet, OnErrorHandler fn)
    //    {
    //        m_fnErr = fn;
    //        Deserialize(packet);
    //        if (m_eErrorCode != eErrorCode.Success)
    //        {
    //            if (null != m_fnErr)
    //                m_fnErr(string.Empty, m_eErrorCode);

    //            return false;
    //        }

    //        return true;
    //    }
    //}

    //public delegate void OnErrorHandler(string str, eErrorCode err);

    public class PT_CA_Auth_Login_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public string m_strToken = string.Empty;
        public string m_strIP = string.Empty;
        public int m_nPort = 0;

        public PT_CA_Auth_Login_RS()
        {
            m_packetType = PacketType.PT_CA_Auth_Login_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);
            if (eErrorCode.Success != m_eErrorCode)
                return;

            p.WriteString(m_strToken);
            p.WriteString(m_strIP);
            p.WriteInt(m_nPort);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();
            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_strToken = p.ReadString();
            m_strIP = p.ReadString();
            m_nPort = p.ReadInt();
        }
        
        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_strToken = string.Empty;
            m_strIP = string.Empty;
            m_nPort = 0;
        }
    }

    public class PT_CA_Auth_GetWorldList_RQ : fmPacket
    {
        public PT_CA_Auth_GetWorldList_RQ()
        {
            m_packetType = PacketType.PT_CA_Auth_GetWorldList_RQ;
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

    public class PT_CA_Auth_GetWorldList_RS : fmPacket
    {
        public eErrorCode m_eErrorCode;
        public List<fmWorld> m_list = null;

        public PT_CA_Auth_GetWorldList_RS()
        {
            m_packetType = PacketType.PT_CA_Auth_GetWorldList_RS;
            m_eErrorCode = eErrorCode.Error;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_list.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (m_eErrorCode != eErrorCode.Success)
                return;

            if (null == m_list)
                m_list = new List<fmWorld>();

            m_list.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

    public class PT_CG_Auth_GetConstant_RQ : fmPacket
    {
        public PT_CG_Auth_GetConstant_RQ()
        {
            m_packetType = PacketType.PT_CG_Auth_GetConstant_RQ;
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

    public class PT_CG_Auth_GetConstant_RS : fmPacket
    {
        public eErrorCode m_eErrorCode;
        public fmGameConst m_gameconst = null;

        public PT_CG_Auth_GetConstant_RS()
        {
            m_packetType = PacketType.PT_CG_Auth_GetConstant_RS;
            m_eErrorCode = eErrorCode.Error;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_gameconst.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (m_eErrorCode != eErrorCode.Success)
                return;

            m_gameconst = new fmGameConst();
            m_gameconst.Read(ref p);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            if (null != m_gameconst)
            {
                using (m_gameconst) { }
                m_gameconst = null;
            }
        }
    }
}
