namespace TimeSeriesDatabaseEngine;

public struct PatternMatch<TTimestamp>
{
    public TTimestamp Start { get; set; }
    public TTimestamp End { get; set; }
    public double Similarity { get; set; }
}
