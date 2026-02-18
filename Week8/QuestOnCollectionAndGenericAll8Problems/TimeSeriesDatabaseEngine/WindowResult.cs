namespace TimeSeriesDatabaseEngine;

public struct WindowResult<TTimestamp, TAggregate>
{
    public TTimestamp Start { get; set; }
    public TTimestamp End { get; set; }
    public TAggregate Aggregate { get; set; }
}
