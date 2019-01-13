using System;

namespace fmCommon
{
    public class Packet_XXX : fmPacket
    {
        public int m_nXXX = 0;

        public Packet_XXX()
        {
            m_packetType = PacketType.PT_Unkwon;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(m_nXXX);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_nXXX = p.ReadInt();
        }

        protected override void Reset()
        {
            m_nXXX = 0;
        }
    }


    public class PT_Test_RQ : fmPacket
    {
        public int id = 0;
        public int seq = 0;

        public PT_Test_RQ()
        {
            m_packetType = PacketType.PT_Test_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt(id);
            p.WriteInt(seq);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            id = p.ReadInt();
            seq = p.ReadInt();
        }

        protected override void Reset()
        {
            id = 0;
            seq = 0;
        }
    }

    public class PT_Test_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_Test_RS()
        {
            m_packetType = PacketType.PT_Test_RS;
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
