using System.Collections.Generic;

namespace RealTimeStockTradingPlatform;

public class CircularBuffer<T>(int capacity)
{
    private readonly T[] _buffer = new T[capacity];
    private int _index;
    private int _count;

    public void Add(T item)
    {
        _buffer[_index] = item;
        _index = (_index + 1) % _buffer.Length;
        if (_count < _buffer.Length) _count++;
    }

    public IEnumerable<T> Snapshot()
    {
        if (_count < _buffer.Length)
        {
            for (int i = 0; i < _count; i++)
                yield return _buffer[i];
        }
        else
        {
            for (int i = 0; i < _buffer.Length; i++)
                yield return _buffer[(_index + i) % _buffer.Length];
        }
    }

    public int Count => _count;
}
