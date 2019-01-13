using System;

namespace fmServerCommon
{
    public class Singleton<T> where T : class, new()
    {
        private static object m_objcetLock = new object();

        private static T m_instance = null;

        public static T Instance
        {
            get
            {
                lock (m_objcetLock)
                {
                    if (null == m_instance)
                    {
                        m_instance = new T();
                    }
                }

                return m_instance;
            }
        }
    }
}
