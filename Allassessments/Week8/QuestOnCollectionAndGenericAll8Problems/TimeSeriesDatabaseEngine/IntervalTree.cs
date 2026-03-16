using System.Collections.Generic;

namespace TimeSeriesDatabaseEngine;

public class IntervalTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private readonly List<(TKey Start, TKey End, TValue Value)> _intervals = new();

    public void Add(TKey start, TKey end, TValue value)
    {
        _intervals.Add((start, end, value));
    }

    public IEnumerable<TValue> Query(TKey point)
    {
        foreach (var (start, end, value) in _intervals)
        {
            if (point.CompareTo(start) >= 0 && point.CompareTo(end) <= 0)
                yield return value;
        }
    }
}
