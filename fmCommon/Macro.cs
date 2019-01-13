namespace fmCommon
{
    public static class Macro
    {
        public static int HIWORD(int x) { return x >> 16; }
        public static int LOWORD(int x) { return x << 16; }


        public static eOptGrade GetOptHiword(eOption opt)
        {
            return (eOptGrade)((int)opt >> 8);
        }
    }
}



