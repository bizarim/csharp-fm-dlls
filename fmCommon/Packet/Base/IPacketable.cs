namespace fmCommon
{
    /// <summary>
    /// Packetable interface
    /// 목적:
    ///     1. Packet 모델
    ///     2. Packet 읽고 쓰기
    /// </summary>
    public interface IPacketable
    {
        void Read(ref Packet p);
        void Write(ref Packet p);
    }
}
