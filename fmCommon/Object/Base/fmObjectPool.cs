using System;
using System.Collections.Generic;

namespace fmCommon
{
    //public class fmObjectPool<T> where T : fmObject, new()
    //{
    //    private static ThreadSafeQueue m_pool = new ThreadSafeQueue();
    //    //public delegate void FREE(fmObject parent);
    //    //public FREE m_fnFree = null;

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

    //    public static void Free(fmObject item)
    //    {
    //        if (item == null)
    //            return;
    //        item.SetFnFree(null);
    //        m_pool.Enqueue((T)item);
    //    }
    //}
}
