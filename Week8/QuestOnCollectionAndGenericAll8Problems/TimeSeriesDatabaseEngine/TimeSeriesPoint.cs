namespace TimeSeriesDatabaseEngine;

public struct TimeSeriesPoint<TTimestamp, TValue>
    where TTimestamp : IComparable<TTimestamp>, IEquatable<TTimestamp>
    where TValue : struct
{
    public TTimestamp Timestamp { get; set; }
    public TValue Value { get; set; }
}
