using System.Collections;

namespace fmServerCommon
{
    public class ThreadSafeQueue
    {
        private object m_lockObjcet = new object();
        private Queue m_queue = new Queue();

        public object Dequeue()
        {
            lock (m_lockObjcet)
            {
                if (m_queue.Count <= 0)
                    return null;

                return m_queue.Dequeue();
            }
        }

        public void Enqueue(object item)
        {
            lock (m_lockObjcet)
            {
                m_queue.Enqueue(item);
            }
        }
    }
}
