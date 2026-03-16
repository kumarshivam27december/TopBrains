namespace TimeSeriesDatabaseEngine;

public class SegmentedBitArray(int segmentSize = 10000)
{
    private readonly List<bool[]> _segments = new();
    private readonly int _segmentSize = segmentSize;
    private int _count;

    public int Count => _count;

    public void Add(bool value)
    {
        int segmentIndex = _count / _segmentSize;
        int indexInSegment = _count % _segmentSize;
        if (indexInSegment == 0)
            _segments.Add(new bool[_segmentSize]);
        _segments[segmentIndex][indexInSegment] = value;
        _count++;
    }

    public bool this[int index]
    {
        get
        {
            int si = index / _segmentSize;
            int ii = index % _segmentSize;
            return _segments[si][ii];
        }
    }
}
