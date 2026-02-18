namespace RealTimeStockTradingPlatform;

public class MarketData<T> where T : IComparable<T>
{
    public T Instrument { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; set; }
}
