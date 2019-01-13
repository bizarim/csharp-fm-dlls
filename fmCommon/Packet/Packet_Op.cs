using System.Collections.Generic;

namespace fmCommon
{
    public class PT_OG_ModifyInformation_RQ : fmPacket
    {
        public string Name { get; set; }
        public long AccId { get; set; }
        public List<fmOpInfo> ModiInfos = null;

        public PT_OG_ModifyInformation_RQ()
        {
            m_packetType = PacketType.PT_OG_ModifyInformation_RQ;
            Name = string.Empty;
            AccId = 0;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);

            if (null == ModiInfos)
                ModiInfos = new List<fmOpInfo>();

            p.WriteString(Name);
            p.WriteLong(AccId);
            ModiInfos.Write(ref p);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);

            if (null == ModiInfos)
                ModiInfos = new List<fmOpInfo>();
            Name = p.ReadString();
            AccId = p.ReadLong();
            ModiInfos.Read(ref p);
        }

        protected override void Reset()
        {
            ModiInfos = null;
            Name = string.Empty;
            AccId = 0;
        }
    }

    public class PT_OG_ModifyInformation_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_OG_ModifyInformation_RS()
        {
            m_packetType = PacketType.PT_OG_ModifyInformation_RS;
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

    public class PT_OG_Gift_RQ : fmPacket
    {
        public string Name { get; set; }
        public List<fmOpInfo> ModiInfos = null;

        public PT_OG_Gift_RQ()
        {
            m_packetType = PacketType.PT_OG_Gift_RQ;
            Name = string.Empty;
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
            Name = string.Empty;
        }
    }

    public class PT_OG_Gift_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_OG_Gift_RS()
        {
            m_packetType = PacketType.PT_OG_Gift_RS;
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
}
