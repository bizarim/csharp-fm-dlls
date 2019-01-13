
namespace fmCommon
{
    public class PT_CG_IAB_Prepare_RQ : fmPacket
    {
        public eAppOs AppOs { get; set; }
        public int Code { get; set; }

        public PT_CG_IAB_Prepare_RQ()
        {
            m_packetType = PacketType.PT_CG_IAB_Prepare_RQ;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)AppOs);
            p.WriteInt(Code);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            AppOs = (eAppOs)p.ReadInt();
            Code = p.ReadInt();
        }

        protected override void Reset()
        {
            AppOs = eAppOs.Google;
            Code = 0;
        }
    }

    public class PT_CG_IAB_Prepare_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;
        public string Token { get; set; }

        public PT_CG_IAB_Prepare_RS()
        {
            m_packetType = PacketType.PT_CG_IAB_Prepare_RS;
            Token = string.Empty;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

            p.WriteString(Token);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;
            Token = p.ReadString();
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            Token = string.Empty;
        }
    }

    public class PT_CG_IAB_Cancel_RQ : fmPacket
    {
        public PT_CG_IAB_Cancel_RQ()
        {
            m_packetType = PacketType.PT_CG_IAB_Cancel_RQ;
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

    public class PT_CG_IAB_Cancel_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public PT_CG_IAB_Cancel_RS()
        {
            m_packetType = PacketType.PT_CG_IAB_Cancel_RS;
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

    public class PT_CG_IAB_Purchase_RQ : fmPacket
    {
        public string Token { get; set; }
        public string ReceiptData   { get; set; }
        public string SignatureData { get; set; }

        public PT_CG_IAB_Purchase_RQ()
        {
            m_packetType = PacketType.PT_CG_IAB_Purchase_RQ;
            Token = string.Empty;
            ReceiptData = string.Empty;
            SignatureData = string.Empty;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteString(Token);
            p.WriteString(ReceiptData);
            p.WriteString(SignatureData);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            Token = p.ReadString();
            ReceiptData = p.ReadString();
            SignatureData = p.ReadString();
        }

        protected override void Reset()
        {
            Token = string.Empty;
            ReceiptData = string.Empty;
            SignatureData = string.Empty;
        }
    }

    public class PT_CG_IAB_Purchase_RS : fmPacket
    {
        public eErrorCode m_eErrorCode = eErrorCode.Error;

        public rdLordInfo LordInfo { get; set; }
        public rdItem Item { get; set; }
        public int Ruby { get; set; }

        public PT_CG_IAB_Purchase_RS()
        {
            m_packetType = PacketType.PT_CG_IAB_Purchase_RS;
        }

        public override void Serialize(Packet p)
        {
            base.Serialize(p);
            p.WriteInt((int)m_eErrorCode);

            if (eErrorCode.Success != m_eErrorCode)
                return;

            LordInfo.Write(ref p);
            Item.Write(ref p);
            p.WriteInt(Ruby);
        }

        public override void Deserialize(Packet p)
        {
            base.Deserialize(p);
            m_eErrorCode = (eErrorCode)p.ReadInt();

            if (eErrorCode.Success != m_eErrorCode)
                return;

            LordInfo = new rdLordInfo();
            Item = new rdItem();

            LordInfo.Read(ref p);
            Item.Read(ref p);
            Ruby = p.ReadInt();
        }

        protected override void Reset()
        {
            m_eErrorCode = eErrorCode.Error;
            LordInfo = null;
            Item = null;
            Ruby = 0;
        }
    }
}
