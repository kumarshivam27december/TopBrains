namespace RealTimeStockTradingPlatform;

public class ConcreteOrder : IOrder<string>
{
    public string OrderId { get; set; } = string.Empty;
    public string Instrument { get; set; } = string.Empty;
    public OrderSide Side { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; set; }
    public int Priority { get; set; }
}
