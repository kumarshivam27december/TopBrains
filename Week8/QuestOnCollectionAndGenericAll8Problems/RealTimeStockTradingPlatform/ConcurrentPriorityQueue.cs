namespace RealTimeStockTradingPlatform;

public class ConcurrentPriorityQueue<T>
{
    private readonly PriorityQueue<T, T> _queue;
    private readonly object _lock = new();

    public ConcurrentPriorityQueue(IComparer<T> comparer)
    {
        var priorityComparer = Comparer<T>.Create((a, b) => comparer.Compare(a, b));
        _queue = new PriorityQueue<T, T>(priorityComparer);
    }

    public void Enqueue(T item)
    {
        lock (_lock)
        {
            _queue.Enqueue(item, item);
        }
    }

    public bool TryDequeue(out T? item)
    {
        lock (_lock)
        {
            if (_queue.Count > 0)
            {
                item = _queue.Dequeue();
                return true;
            }
            item = default;
            return false;
        }
    }

    public bool TryPeek(out T? item)
    {
        lock (_lock)
        {
            if (_queue.Count > 0)
            {
                item = _queue.Peek();
                return true;
            }
            item = default;
            return false;
        }
    }

    public int Count { get { lock (_lock) return _queue.Count; } }
}
