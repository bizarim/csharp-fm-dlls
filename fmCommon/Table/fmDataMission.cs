
namespace fmCommon
{
    public class fmDataMission : fmData
    {
        public int          m_nCode = 0;
        public eMission     m_eMission = eMission.None;
        public int          m_nCondition = 0;
        public string       m_strContents = string.Empty;
        //public int          m_nExp = 0;
        public eReward      m_eReward = 0;
        public int          m_nValue = 0;

        public fmDataMission() { m_eFmDataType = eFmDataType.Mission; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_eMission, sizeof(int));
            coder.EncodeDecode(eType, ref m_nCondition, sizeof(int));
            coder.EncodeDecode(eType, ref m_strContents);
            //coder.EncodeDecode(eType, ref m_nExp);
            coder.EncodeDecode(eType, ref m_eReward, sizeof(int));
            coder.EncodeDecode(eType, ref m_nValue);
        }
    }
}
