using fmLibrary;

namespace fmServerCommon
{
    public class appServer : Server
    {
        protected eState m_eState;
        public void SetState(eState state) { m_eState = state; }
        public eState GetState() { return m_eState; }

        protected eServerType m_eServerType;
        public eServerType GetServerType() { return m_eServerType; }

        public string dbAcc() {  return m_config.m_db.m_myAcc; }
        public string dbLog() {  return m_config.m_db.m_myLog; }
        public string rdToken(){ return m_config.m_db.m_rdToken; }
        public string rdGame() { return m_config.m_db.m_rdGame; }

        public int m_nCver = 0;
        public int m_nSver = 0;

        public bool CheckCver(int cVer)
        {
            if (m_config.m_nCver <= cVer)
                return true;

            return false;
        }

        public bool CheckSver(string sVer)
        {
            return true;
        }

        TokenGenerater m_tokenGenerater = new TokenGenerater();
        public string GetToken(string key = "FM") { return m_tokenGenerater.Get(key); }


        public string GetPubKey() { return m_config.m_IABGoogle.m_strPubKey; }
        public string GetExponent() { return m_config.m_IABGoogle.m_strExponent; }
        public string GetModulus() { return m_config.m_IABGoogle.m_strModulus; }

        public bool GetAPNSSendBox() { return false; }
    }
}
