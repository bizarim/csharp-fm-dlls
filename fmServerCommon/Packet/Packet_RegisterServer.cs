using fmCommon;
using System.Collections.Generic;

namespace fmServerCommon
{
    public class PT_AC_Server_GetWorldList_RQ : fmProtocol
    {
        public PT_AC_Server_GetWorldList_RQ() { m_eProtocolType = eProtocolType.PT_AC_Server_GetWorldList_RQ; }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);

        }

        protected override void Dispose(bool disposing) { Reset(); base.Dispose(disposing); }
        protected override void Reset() { }
    }

    public class PT_AC_Server_GetWorldList_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<fmWorld> m_list = null;

        public PT_AC_Server_GetWorldList_RS()
        {
            m_eProtocolType = eProtocolType.PT_AC_Server_GetWorldList_RS;
            m_eErrorCode = eErrorCode.Error;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_list.Write(ref p);    
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            if (null == m_list)
                m_list = new List<fmWorld>();

            m_list.Read(ref p);
        }

        protected override void Dispose(bool disposing)
        {
            Reset();
            base.Dispose(disposing);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            if (null != m_list)
            {
                m_list.Clear();
                m_list = null;
            }
        }
    }

    public class PT_CA_Server_GetWorldList_NT : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public List<fmWorld> m_list = null;

        public PT_CA_Server_GetWorldList_NT()
        {
            m_eProtocolType = eProtocolType.PT_CA_Server_GetWorldList_NT;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_list.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            if (null == m_list)
                m_list = new List<fmWorld>();

            m_list.Read(ref p);
        }

        protected override void Dispose(bool disposing)
        {
            Reset();
            base.Dispose(disposing);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            if (null != m_list)
            {
                m_list.Clear();
                m_list = null;
            }
        }
    }

    public class PT_Server_ReadyToStart_RQ : fmProtocol
    {
        public PT_Server_ReadyToStart_RQ() { m_eProtocolType = eProtocolType.PT_Server_ReadyToStart_RQ; }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);

        }

        protected override void Dispose(bool disposing) { Reset(); base.Dispose(disposing); }
        protected override void Reset() { }
    }

    public class PT_Server_ReadyToStart_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode;

        public PT_Server_ReadyToStart_RS()
        {
            m_eProtocolType = eProtocolType.PT_Server_ReadyToStart_RS;
            m_eErrorCode = eErrorCode.Error;
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

        protected override void Dispose(bool disposing)
        {
            Reset();
            base.Dispose(disposing);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Success;
        }
    }

    public class PT_Server_RegisterAtCenter_RQ : fmProtocol
    {
        public eServerType m_eServerType = eServerType.None;
        public int m_nSequence = 0;
        public string m_strIP = string.Empty;
        public int m_nPort = 0;

        public PT_Server_RegisterAtCenter_RQ() { m_eProtocolType = eProtocolType.PT_Server_RegisterAtCenter_RQ; }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eServerType);
            p.WriteInt(m_nSequence);
            p.WriteString(m_strIP);
            p.WriteInt(m_nPort);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eServerType = (eServerType)p.ReadInt();
            m_nSequence = p.ReadInt();
            m_strIP = p.ReadString();
            m_nPort = p.ReadInt();
        }

        protected override void Dispose(bool disposing) { Reset(); base.Dispose(disposing); }
        protected override void Reset() { m_eServerType = eServerType.None; }
    }

    public class PT_Server_RegisterAtCenter_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode;

        public PT_Server_RegisterAtCenter_RS()
        {
            m_eProtocolType = eProtocolType.PT_Server_RegisterAtCenter_RS;
            m_eErrorCode = eErrorCode.Error;
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

        protected override void Dispose(bool disposing)
        {
            Reset();
            base.Dispose(disposing);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Success;
        }
    }

    public class PT_Server_UpdateWorldState_NT : fmProtocol
    {
        public eServerType m_eServerType = eServerType.None;
        public int m_nSequence = 0;
        public int m_nPlayerCount = 0;
        //public eWorldState m_eWorldState = eWorldState.Check;

        public PT_Server_UpdateWorldState_NT() { m_eProtocolType = eProtocolType.PT_Server_UpdateWorldState_NT; }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eServerType);
            p.WriteInt(m_nSequence);
            p.WriteInt(m_nPlayerCount);
            //p.WriteInt((int)m_eWorldState);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eServerType = (eServerType)p.ReadInt();
            m_nSequence = p.ReadInt();
            m_nPlayerCount = p.ReadInt();
            //m_eWorldState = (eWorldState)p.ReadInt();
        }

        protected override void Dispose(bool disposing) { Reset(); base.Dispose(disposing); }
        protected override void Reset() { m_eServerType = eServerType.None; }
    }

    public class PT_Server_RegisterAtChat_RQ : fmProtocol
    {
        public eServerType m_eServerType = eServerType.None;
        public int m_nSequence = 0;
        public string m_strIP = string.Empty;
        public int m_nPort = 0;

        public PT_Server_RegisterAtChat_RQ()
        {
            m_eProtocolType = eProtocolType.PT_Server_RegisterAtChat_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eServerType);
            p.WriteInt(m_nSequence);
            p.WriteString(m_strIP);
            p.WriteInt(m_nPort);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eServerType = (eServerType)p.ReadInt();
            m_nSequence = p.ReadInt();
            m_strIP = p.ReadString();
            m_nPort = p.ReadInt();
        }

        protected override void Dispose(bool disposing) { Reset(); base.Dispose(disposing); }
        protected override void Reset() { m_eServerType = eServerType.None; }
    }

    public class PT_Server_RegisterAtChat_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_Server_RegisterAtChat_RS()
        {
            m_eProtocolType = eProtocolType.PT_Server_RegisterAtChat_RS;
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

        protected override void Dispose(bool disposing)
        {
            Reset();
            base.Dispose(disposing);
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Success;
        }
    }

}
