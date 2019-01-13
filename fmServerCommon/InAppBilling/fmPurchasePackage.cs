using fmCommon;
using System;

namespace fmServerCommon.InAppBilling
{
    public enum eIABState
    {
        None = 0,
        Prepare = 1,
        Purchase = 2,
        Cancel = 3,
        Fail = 4,
    }

    public class fmIABPackage
    {
        public eAppOs AppOs { get; set; }
        public int Code { get; set; }
        public string Token { get; set; }
        public DateTime Time { get; set; }
        public eIABState State { get; set; }

        public void Prepare(eAppOs appos, int code, string token)
        {
            AppOs = appos;
            Code = code;
            Token = token;
            State = eIABState.Prepare;
            Time = fmServerTime.Now;
        }

        public void Cancel()
        {
            Initialize();
        }

        public void Purchase()
        {
            Initialize();
        }

        public void Fail()
        {
            Initialize();
        }

        private void Initialize()
        {
            AppOs = eAppOs.Google;
            Code = 0;
            Token = string.Empty;
            State = eIABState.None;
            Time = fmServerTime.Epoch;
        }

        public bool CanPrepare()
        {
            if (State == eIABState.None)
                return true;

            return false;
        }

        public bool CheckPrepare()
        {
            if (State == eIABState.Prepare)
                return true;

            return false;
        }

        public bool CheckToken(string token)
        {
            if (true == string.IsNullOrEmpty(Token))
                return false;

            if (true == string.IsNullOrEmpty(token))
                return false;

            return Token.Equals(token);
        }
    }
}
