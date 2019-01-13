using System;
using System.Collections.Generic;

namespace fmCommon
{
    /// <summary>
    /// 프로토콜 Pool
    /// 목적:
    ///     c++처럼 pool을 사용하려고 했으나
    ///     c#에서는 사용하지 않는게 더 성능상 좋다고 판단됨
    /// </summary>
    //public class fmProtocolPool<T> where T : fmProtocol, new()
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

    //    public static void Free(fmProtocol item)
    //    {
    //        if (item == null)
    //            return;

    //        item.SetFnFree(Free);
    //        m_pool.Enqueue((T)item);
    //    }
    //}
}
