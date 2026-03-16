using System.Collections.Generic;

namespace GeneticAlgorithmFramework;

public class AdaptiveSortedList<TKey, TValue> where TKey : IComparable<TKey>
{
    private readonly SortedList<TKey, TValue> _list = new();

    public void Add(TKey key, TValue value)
    {
        _list[key] = value;
    }

    public IEnumerable<TValue> GetTop(int n)
    {
        return _list.Values.Take(n);
    }

    public int Count => _list.Count;
}
