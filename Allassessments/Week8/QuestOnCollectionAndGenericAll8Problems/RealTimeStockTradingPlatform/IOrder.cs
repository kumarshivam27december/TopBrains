namespace RealTimeStockTradingPlatform;

public enum OrderSide { Buy, Sell }

public interface IOrder<T> where T : IComparable<T>
{
    string OrderId { get; }
    T Instrument { get; }
    OrderSide Side { get; }
    decimal Price { get; }
    int Quantity { get; }
    DateTime Timestamp { get; }
    int Priority { get; }
}
