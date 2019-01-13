using System;

namespace fmCommon
{
    // fmData 만들기 순서 02
    // fmDataXXX 클래스를 추가한다.
    public class fmDataXXX : fmData
    {
        public fmDataXXX() { }
        protected override int GetCode() { return 0; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder) { }
    }

    // fmData 상속 구조 일때 사용 방법
    public class fmDataYYY : fmDataXXX
    {
        public fmDataYYY() : base() { }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder) { base.EncodeDecode(eType, coder); }
    }
}
