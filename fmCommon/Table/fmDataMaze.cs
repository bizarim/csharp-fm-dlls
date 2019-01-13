using System;
using System.Collections.Generic;
namespace fmCommon
{
    public class fmDataMaze : fmData
    {
        public int      m_nCode             = 0;
        public int      m_nFloor            = 0;

        public int[]    m_nArrAppearMon     = null;
        public int[]    m_nArrAppearRateMon = null;

        public fmDataMaze() { m_eFmDataType = eFmDataType.Maze; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_nFloor);

            coder.EncodeDecode(eType, ref m_nArrAppearMon);
            coder.EncodeDecode(eType, ref m_nArrAppearRateMon);
        }
    }
}
