using System;
using System.Collections.Generic;
using System.Threading;

namespace fmServerCommon
{
    public class ConcurrentBatchQueue<T> : IBatchQueue<T>
    {
        private Entity m_Entity;
        private Entity m_BackEntity;

        private static readonly T m_Null = default(T);

        private Func<T, bool> m_NullValidator;

        class Entity
        {
            public T[] Array { get; set; }
            public int Count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBatchQueue&lt;T&gt;"/> class.
        /// </summary>
        public ConcurrentBatchQueue()
            : this(16)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBatchQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity of the queue.</param>
        public ConcurrentBatchQueue(int capacity)
            : this(new T[capacity])
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBatchQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="nullValidator">The null validator.</param>
        public ConcurrentBatchQueue(int capacity, Func<T, bool> nullValidator)
            : this(new T[capacity], nullValidator)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBatchQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public ConcurrentBatchQueue(T[] array)
            : this(array, (t) => t == null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBatchQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="nullValidator">The null validator.</param>
        public ConcurrentBatchQueue(T[] array, Func<T, bool> nullValidator)
        {
            m_Entity = new Entity();
            m_Entity.Array = array;

            m_BackEntity = new Entity();
            m_BackEntity.Array = new T[array.Length];

            m_NullValidator = nullValidator;
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Enqueue(T item)
        {
            bool full;

            while (true)
            {
                if (TryEnqueue(item, out full) || full)
                    break;
            }

            return !full;
        }

        private bool TryEnqueue(T item, out bool full)
        {
            full = false;

            EnsureNotRebuild();

            var entity = m_Entity;
            var array = entity.Array;
            var count = entity.Count;

            if (count >= array.Length)
            {
                full = true;
                return false;
            }

            if (entity != m_Entity)
                return false;

            // entity.Count와 count가 같으면 1을 증가해라
            //  반환값은 아래 함수 수행당시 entity.Count의 값이다.
            int oldCount = Interlocked.CompareExchange(ref entity.Count, count + 1, count);

            // 이 메소드가 처음으로 쭉 호출되었다면, oldCount와 count가 일치할것이다.
            //  그렇지 않다면 다른 쓰레드에서 entity.Count가 1이 증가된 상태이니 oldCount가 처음에 얻어온 count와 다르게 될것이다.
            if (oldCount != count)
                return false;

            array[count] = item;

            return true;
        }

        /// <summary>
        /// Enqueues the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public bool Enqueue(IList<T> items)
        {
            bool full;

            while (true)
            {
                if (TryEnqueue(items, out full) || full)
                    break;
            }

            return !full;
        }

        private bool TryEnqueue(IList<T> items, out bool full)
        {
            full = false;

            var entity = m_Entity;
            var array = entity.Array;
            var count = entity.Count;

            int newItemCount = items.Count;
            int expectedCount = count + newItemCount;

            if (expectedCount > array.Length)
            {
                full = true;
                return false;
            }

            if (entity != m_Entity)
                return false;

            int oldCount = Interlocked.CompareExchange(ref entity.Count, expectedCount, count);

            if (oldCount != count)
                return false;

            foreach (var item in items)
            {
                array[count++] = item;
            }

            return true;
        }

        private void EnsureNotRebuild()
        {
            // m_Rebuilding는 초기값이 false이고, 변경하지 않는다... 그래서 이 함수는 호출하나마나한거 아닌겨?
            if (!m_Rebuilding)
                return;

            var spinWait = new SpinWait();

            while (true)
            {
                spinWait.SpinOnce();

                if (!m_Rebuilding)
                    break;
            }
        }

        private bool m_Rebuilding = false;

        /// <summary>
        /// Tries the dequeue.
        /// </summary>
        /// <param name="outputItems">The output items.</param>
        /// <returns></returns>
        public bool TryDequeue(IList<T> outputItems)
        {
            var entity = m_Entity;
            int count = entity.Count;

            if (count <= 0)
                return false;

            // 호출하는 쓰레드마다 독립적으로 쓰기 위해 로컬변수처럼 쓴다.            
            var spinWait = new SpinWait();

            // m_Entity를 데큐하기전에 엔큐못하게 객체를 스위칭한다.
            Interlocked.Exchange(ref m_Entity, m_BackEntity);

            // sleep효과이고, 단지 컨텍스트스위칭없이 일정시간대기하는거다.
            //  entity.Count를 다시 좀더 정확하게 읽어오기 위해 아주 짧은시간 대기를 한다.
            spinWait.SpinOnce();

            // 다시 카운트를 읽어온다.
            count = entity.Count;

            var array = entity.Array;

            var i = 0;

            while (true)
            {
                var item = array[i];

                // item이 null이면 true?
                //  엔큐에서 카운트를 먼저 증가시키고, 내용을 추가한다.
                //  그래서 실제 내용이 추가될때까지 기다린다.
                //  a = b; 와 같은 문맥은 아토믹하게 처리된다는 원리를 이용해서 락을 걸고 있지 않다.

                while (m_NullValidator(item))
                {
                    // 해당 노드에 레퍼런스가 추가되었는지 다시 읽어오기 위해 아주 짧은시간 대기를 한다.(딜레이 없이 곧바로 다시 체크하면 실패할 가능성이 높기때문이다.)
                    spinWait.SpinOnce();
                    item = array[i];
                }

                outputItems.Add(array[i]);
                array[i] = m_Null;


                if (entity.Count <= (i + 1))
                    break;

                i++;
            }

            m_BackEntity = entity;
            m_BackEntity.Count = 0;

            return true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return m_Entity.Count <= 0; }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return m_Entity.Count; }
        }
    }
}
