//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;

namespace fmServerCommon
{
    //public class ConcurrentList<T> : IList<T>
    //{
    //    private List<T> m_list;

    //    private readonly object m_objLock = new object();

    //    private ConcurrentList()
    //    {
    //        m_list = new List<T>();
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return Clone().GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return Clone().GetEnumerator();
    //    }

    //    public List<T> Clone()
    //    {
    //        ThreadLocal<List<T>> threadClonedList = new ThreadLocal<List<T>>();

    //        lock (m_objLock)
    //        {
    //            m_list.ForEach(element => { threadClonedList.Value.Add(element); });
    //        }

    //        return (threadClonedList.Value);
    //    }

    //    public void Add(T item)
    //    {
    //        lock (m_objLock)
    //        {
    //            m_list.Add(item);
    //        }
    //    }

    //    public bool Remove(T item)
    //    {
    //        bool isRemoved;

    //        lock (m_objLock)
    //        {
    //            isRemoved = m_list.Remove(item);
    //        }

    //        return (isRemoved);
    //    }

    //    public void Clear()
    //    {
    //        lock (m_objLock)
    //        {
    //            m_list.Clear();
    //        }
    //    }

    //    public bool Contains(T item)
    //    {
    //        bool containsItem;

    //        lock (m_objLock)
    //        {
    //            containsItem = m_list.Contains(item);
    //        }

    //        return (containsItem);
    //    }

    //    public void CopyTo(T[] array, int arrayIndex)
    //    {
    //        lock (m_objLock)
    //        {
    //            m_list.CopyTo(array, arrayIndex);
    //        }
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            int count;

    //            lock (m_objLock)
    //            {
    //                count = m_list.Count;
    //            }

    //            return (count);
    //        }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { return false; }
    //    }

    //    public int IndexOf(T item)
    //    {
    //        int itemIndex;

    //        lock (m_objLock)
    //        {
    //            itemIndex = m_list.IndexOf(item);
    //        }

    //        return (itemIndex);
    //    }

    //    public void Insert(int index, T item)
    //    {
    //        lock (m_objLock)
    //        {
    //            m_list.Insert(index, item);
    //        }
    //    }

    //    public void RemoveAt(int index)
    //    {
    //        lock (m_objLock)
    //        {
    //            m_list.RemoveAt(index);
    //        }
    //    }

    //    public T this[int index]
    //    {
    //        get
    //        {
    //            lock (m_objLock)
    //            {
    //                return m_list[index];
    //            }
    //        }
    //        set
    //        {
    //            lock (m_objLock)
    //            {
    //                m_list[index] = value;
    //            }
    //        }
    //    }
    //}
}
