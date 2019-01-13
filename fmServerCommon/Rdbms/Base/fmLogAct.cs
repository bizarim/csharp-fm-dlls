using fmCommon;
using System;

namespace fmServerCommon
{
    public class fmLogAct
    {
        public DateTime         Time { get; set; }
        public eProtocolType    PType { get; set; }

        public long AccId   { get; set; }
        public int  Lv      { get; set; }
        public long Gold    { get; set; }
        public int  C1      { get; set; }
        public int  C2      { get; set; }
    }
}
