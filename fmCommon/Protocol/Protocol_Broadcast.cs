using System.Collections.Generic;

namespace fmCommon
{
    public class PT_GC_Broadcast_Public_NT : fmProtocol
    {
        protected virtual byte[] Compress(Packet p) { return null; }
        protected virtual Packet Decompress(Packet p) { return p; }

        public string m_strName = string.Empty;
        public string m_strContents = string.Empty;       

        public PT_GC_Broadcast_Public_NT()
        {
            m_eProtocolType = eProtocolType.PT_GC_Broadcast_Public_NT;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            Packet packet = new Packet(m_eProtocolType);
            
            packet.WriteString(m_strName);
            packet.WriteString(m_strContents);

            byte[] bytes = Compress(packet);
            if(null != bytes)
                p.Write(bytes);

            bytes = null;
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_strName = p.ReadString();
            m_strContents = p.ReadString();
        }

        protected override void Reset()
        {
            m_strContents = string.Empty;
        }
    }

    public class PT_OC_Broadcast_Public_NT : fmProtocol
    {
        public string m_strContents = string.Empty;

        public PT_OC_Broadcast_Public_NT()
        {
            m_eProtocolType = eProtocolType.PT_OC_Broadcast_Public_NT;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(m_strContents);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_strContents = p.ReadString();
        }

        protected override void Reset()
        {
            m_strContents = string.Empty;
        }
    }

    public class PT_OC_Broadcast_Private_NT : fmProtocol
    {
        public List<fmOpInfo> ModiInfos = null;

        public PT_OC_Broadcast_Private_NT()
        {
            m_eProtocolType = eProtocolType.PT_OC_Broadcast_Private_NT;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            if (null == ModiInfos)
                ModiInfos = new List<fmOpInfo>();

            ModiInfos.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            if (null == ModiInfos)
                ModiInfos = new List<fmOpInfo>();

            ModiInfos.Read(ref p);
        }

        protected override void Reset()
        {
            ModiInfos = null;
        }
    }

    public class PT_OA_Broadcast_SetNotice_RQ : fmProtocol
    {
        public eLanguage    m_eLanguage = eLanguage.English;
        public string       m_strContents = string.Empty;

        public PT_OA_Broadcast_SetNotice_RQ()
        {
            m_eProtocolType = eProtocolType.PT_OA_Broadcast_SetNotice_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eLanguage);
            p.WriteString(m_strContents);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eLanguage = (eLanguage)p.ReadInt();
            m_strContents = p.ReadString();
        }

        protected override void Reset()
        {
            m_eLanguage = eLanguage.English;
            m_strContents = string.Empty;
        }
    }

    public class PT_OA_Broadcast_SetNotice_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_OA_Broadcast_SetNotice_RS()
        {
            m_eProtocolType = eProtocolType.PT_OA_Broadcast_SetNotice_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
        }
    }

    public class PT_CA_Broadcast_GetNotice_RQ : fmProtocol
    {
        public eLanguage m_eLanguage = eLanguage.English;

        public PT_CA_Broadcast_GetNotice_RQ()
        {
            m_eProtocolType = eProtocolType.PT_CA_Broadcast_GetNotice_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eLanguage);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eLanguage = (eLanguage)p.ReadInt();
        }

        protected override void Reset()
        {
            m_eLanguage = eLanguage.English;
        }
    }

    public class PT_CA_Broadcast_GetNotice_RS : fmProtocol
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public string m_strContents = string.Empty;

        public PT_CA_Broadcast_GetNotice_RS()
        {
            m_eProtocolType = eProtocolType.PT_CA_Broadcast_GetNotice_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

            p.WriteString(m_strContents);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            m_strContents = p.ReadString();
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            m_strContents = string.Empty;
        }
    }
}
