using RealTimeStockTradingPlatform;

var orderBook = new OrderBook<string>();
var order1 = new ConcreteOrder
{
    OrderId = "O1",
    Instrument = "AAPL",
    Side = OrderSide.Buy,
    Price = 150,
    Quantity = 10,
    Timestamp = DateTime.UtcNow,
    Priority = 1
};
var order2 = new ConcreteOrder
{
    OrderId = "O2",
    Instrument = "AAPL",
    Side = OrderSide.Sell,
    Price = 149,
    Quantity = 10,
    Timestamp = DateTime.UtcNow,
    Priority = 1
};

await orderBook.ProcessOrderAsync(order1);
await orderBook.ProcessOrderAsync(order2);

var matches = orderBook.GetOrderMatches(10).ToList();
Console.WriteLine($"Matches: {matches.Count}");
var vwap = orderBook.CalculateVWAP(TimeSpan.FromHours(1));
Console.WriteLine($"VWAP: {vwap}");
