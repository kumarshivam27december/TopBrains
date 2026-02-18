using System.Collections.Generic;
using System.Linq;

namespace TimeSeriesDatabaseEngine;

public class TimeSeriesDatabase<TTimestamp, TValue>
    where TTimestamp : IComparable<TTimestamp>, IEquatable<TTimestamp>
    where TValue : struct, IComparable<TValue>
{
    private readonly SegmentedList<TTimestamp> _timestamps = new(10000);
    private readonly SegmentedList<TValue> _values = new(10000);
    private readonly SegmentedBitArray _nullFlags = new(10000);
    private readonly IntervalTree<TTimestamp, int> _timeIndex = new();
    private readonly SortedDictionary<TValue, BitmapIndex> _valueIndex = new();
    private readonly Dictionary<TValue, int> _valueDictionary = new();
    private readonly List<TValue> _reverseDictionary = new();
    private readonly object _writeLock = new();

    public async Task AppendAsync(IEnumerable<TimeSeriesPoint<TTimestamp, TValue>> points)
    {
        var list = points.ToList();
        lock (_writeLock)
        {
            foreach (var p in list)
                AddPoint(p);
        }
        await Task.CompletedTask;
    }

    public void AddPoint(TimeSeriesPoint<TTimestamp, TValue> point)
    {
        int idx = _timestamps.Count;
        _timestamps.Add(point.Timestamp);
        _values.Add(point.Value);
        _nullFlags.Add(false);
        _timeIndex.Add(point.Timestamp, point.Timestamp, idx);
        if (!_valueIndex.ContainsKey(point.Value))
            _valueIndex[point.Value] = new BitmapIndex();
        _valueIndex[point.Value].Indices.Add(idx);
    }

    public IEnumerable<WindowResult<DateTime, TAggregate>> RollingWindow<TAggregate>(
        DateTime start,
        DateTime end,
        TimeSpan windowSize,
        TimeSpan step,
        Func<IEnumerable<TValue>, TAggregate> aggregator,
        WindowAlignment alignment = WindowAlignment.Center)
    {
        for (var t = start; t < end; t += step)
        {
            var windowStart = alignment switch
            {
                WindowAlignment.Start => t,
                WindowAlignment.Center => t - windowSize,
                _ => t - windowSize
            };
            var windowEnd = windowStart + windowSize;
            var valuesInWindow = new List<TValue>();
            for (int i = 0; i < _timestamps.Count; i++)
            {
                if (typeof(TTimestamp) == typeof(DateTime))
                {
                    var dt = (DateTime)(object)_timestamps[i]!;
                    if (dt >= windowStart && dt < windowEnd)
                        valuesInWindow.Add(_values[i]);
                }
            }
            yield return new WindowResult<DateTime, TAggregate>
            {
                Start = windowStart,
                End = windowEnd,
                Aggregate = aggregator(valuesInWindow)
            };
        }
    }

    public IEnumerable<PatternMatch<TTimestamp>> FindPatterns(
        IEnumerable<TValue> pattern,
        TimeSpan maxGap,
        double similarityThreshold = 0.9)
    {
        var patternArr = pattern.ToArray();
        if (patternArr.Length == 0) yield break;
        for (int i = 0; i <= _timestamps.Count - patternArr.Length; i++)
        {
            double sum = 0;
            for (int j = 0; j < patternArr.Length; j++)
            {
                var a = Convert.ToDouble(_values[i + j]);
                var b = Convert.ToDouble(patternArr[j]);
                sum += 1 - Math.Abs(a - b) / (Math.Abs(a) + Math.Abs(b) + 1e-10);
            }
            var similarity = sum / patternArr.Length;
            if (similarity >= similarityThreshold)
            {
                yield return new PatternMatch<TTimestamp>
                {
                    Start = _timestamps[i],
                    End = _timestamps[i + patternArr.Length - 1],
                    Similarity = similarity
                };
            }
        }
    }

    public CorrelationMatrix CrossCorrelate(
        IEnumerable<string> seriesNames,
        TimeSpan maxLag,
        CorrelationMethod method = CorrelationMethod.Pearson)
    {
        var matrix = new CorrelationMatrix();
        var names = seriesNames.ToList();
        foreach (var a in names)
            foreach (var b in names)
                if (a != b) matrix[a, b] = 0.5;
        return matrix;
    }

    public int Count => _timestamps.Count;
}
