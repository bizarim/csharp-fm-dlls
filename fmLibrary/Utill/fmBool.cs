using System;
using System.Threading;

namespace fmLibrary
{
    // 원자성을 지니고 있다
    // http://msdn.microsoft.com/ko-kr/library/system.threading.interlocked(v=vs.110).aspx

    // A랑 C가 같다면 B로 바꿔라. 다르다면 바뀌지 않으며, 성공실패를 떠나 리턴값은 항상 A의 원래값이다
    // Interlocked.CompareExchange(A, B, C)

    public class fmBool
    {
        private int m_nValue;

        public fmBool() { m_nValue = 0; }

        public bool SetTrue() { return Interlocked.CompareExchange(ref m_nValue, 1, 0) == 0; }
        public bool SetFalse() { return Interlocked.CompareExchange(ref m_nValue, 0, 1) == 1; }

        public void ForceTrue() { Interlocked.Exchange(ref m_nValue, 1); }
        public void ForceFalse() { Interlocked.Exchange(ref m_nValue, 0); }

        public bool IsTrue() { return m_nValue == 1; }
    }
}
