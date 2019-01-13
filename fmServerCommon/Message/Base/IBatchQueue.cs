using System;
using System.Collections.Generic;

namespace fmServerCommon
{
    // A queue interface which can operate in batch
    public interface IBatchQueue<T>
    {
        // Enqueues the specified item.
        bool Enqueue(T item);

        // Enqueues the specified items.
        bool Enqueue(IList<T> items);

        // Tries to dequeue all items in the queue into the output list.
        bool TryDequeue(IList<T> outputItems);

        // Gets a value indicating whether this instance is empty.
        bool IsEmpty { get; }

        // Gets the count.
        int Count { get; }
    }
}
