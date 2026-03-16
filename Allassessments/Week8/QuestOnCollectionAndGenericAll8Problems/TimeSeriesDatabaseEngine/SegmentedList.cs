using System.Collections;

namespace TimeSeriesDatabaseEngine;

public class SegmentedList<T>(int segmentSize = 10000)
{
    private readonly List<T[]> _segments = new();
    private readonly int _segmentSize = segmentSize;
    private int _count;

    public int Count => _count;

    public void Add(T item)
    {
        int segmentIndex = _count / _segmentSize;
        int indexInSegment = _count % _segmentSize;
        if (indexInSegment == 0)
            _segments.Add(new T[_segmentSize]);
        _segments[segmentIndex][indexInSegment] = item;
        _count++;
    }

    public T this[int index]
    {
        get
        {
            int si = index / _segmentSize;
            int ii = index % _segmentSize;
            return _segments[si][ii];
        }
    }

    public IEnumerable<T> Range(int start, int count)
    {
        for (int i = start; i < start + count && i < _count; i++)
            yield return this[i];
    }
}
