namespace RealTimeStockTradingPlatform;

public class OrderMatch<T> where T : IComparable<T>
{
    public IOrder<T> BuyOrder { get; set; } = null!;
    public IOrder<T> SellOrder { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; set; }
}
