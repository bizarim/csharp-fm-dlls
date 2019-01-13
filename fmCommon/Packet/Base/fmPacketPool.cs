using System;
using System.Collections.Generic;

namespace fmCommon
{
    //public class fmPacketPool<T> where T : fmPacket, new()
    //{
    //    private static ThreadSafeQueue m_pool = new ThreadSafeQueue();

    //    public static T AllocT()
    //    {
    //        T item = (T)m_pool.Dequeue();
    //        if (item != null)
    //        {
    //            item.SetFnFree(Free);
    //            return item;
    //        }
    //        item = new T();
    //        item.SetFnFree(Free);
    //        return item;
    //    }

    //    public static void Free(fmPacket item)
    //    {
    //        if (item == null)
    //            return;

    //        item.SetFnFree(Free);
    //        m_pool.Enqueue((T)item);
    //    }
    //}
}
