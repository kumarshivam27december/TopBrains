using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace RealTimeStockTradingPlatform;

public class OrderBook<T> where T : IComparable<T>
{
    private readonly ConcurrentDictionary<string, IOrder<T>> _allOrders = new();
    private readonly ConcurrentPriorityQueue<IOrder<T>> _buyOrders;
    private readonly ConcurrentPriorityQueue<IOrder<T>> _sellOrders;
    private readonly Channel<MarketData<T>> _marketDataStream = Channel.CreateUnbounded<MarketData<T>>();
    private readonly CircularBuffer<decimal> _priceHistory = new(1000);
    private readonly ConcurrentDictionary<TimeSpan, decimal> _volumeByTime = new();
    private readonly List<OrderMatch<T>> _matchHistory = new();
    private readonly object _matchLock = new();

    public OrderBook()
    {
        _buyOrders = new ConcurrentPriorityQueue<IOrder<T>>(Comparer<IOrder<T>>.Create((a, b) =>
        {
            int priorityCompare = a.Priority.CompareTo(b.Priority);
            if (priorityCompare != 0) return -priorityCompare;
            int priceCompare = b.Price.CompareTo(a.Price);
            if (priceCompare != 0) return priceCompare;
            return a.Timestamp.CompareTo(b.Timestamp);
        }));
        _sellOrders = new ConcurrentPriorityQueue<IOrder<T>>(Comparer<IOrder<T>>.Create((a, b) =>
        {
            int priorityCompare = a.Priority.CompareTo(b.Priority);
            if (priorityCompare != 0) return -priorityCompare;
            int priceCompare = a.Price.CompareTo(b.Price);
            if (priceCompare != 0) return priceCompare;
            return a.Timestamp.CompareTo(b.Timestamp);
        }));
    }

    public async Task ProcessOrderAsync(IOrder<T> order)
    {
        _allOrders[order.OrderId] = order;

        if (order.Side == OrderSide.Buy)
        {
            _buyOrders.Enqueue(order);
            await TryMatchAsync();
        }
        else
        {
            _sellOrders.Enqueue(order);
            await TryMatchAsync();
        }

        _priceHistory.Add(order.Price);
        var bucket = TimeSpan.FromMinutes(order.Timestamp.Ticks / TimeSpan.FromMinutes(1).Ticks);
        _volumeByTime.AddOrUpdate(bucket, order.Price * order.Quantity, (_, v) => v + order.Price * order.Quantity);
        await _marketDataStream.Writer.WriteAsync(new MarketData<T>
        {
            Instrument = order.Instrument,
            Price = order.Price,
            Quantity = order.Quantity,
            Timestamp = order.Timestamp
        });
    }

    private async Task TryMatchAsync()
    {
        while (_buyOrders.TryPeek(out var buy) && _sellOrders.TryPeek(out var sell) && buy!.Price >= sell!.Price)
        {
            _buyOrders.TryDequeue(out _);
            _sellOrders.TryDequeue(out _);
            int qty = Math.Min(buy.Quantity, sell.Quantity);
            var matchPrice = buy.Timestamp < sell.Timestamp ? buy.Price : sell.Price;
            lock (_matchLock)
            {
                _matchHistory.Add(new OrderMatch<T>
                {
                    BuyOrder = buy,
                    SellOrder = sell,
                    Price = matchPrice,
                    Quantity = qty,
                    Timestamp = DateTime.UtcNow
                });
            }
            _priceHistory.Add(matchPrice);
            await Task.Yield();
        }
    }

    public IEnumerable<OrderMatch<T>> GetOrderMatches(int count)
    {
        lock (_matchLock)
        {
            return _matchHistory.AsParallel().Take(count).ToList();
        }
    }

    public decimal CalculateVWAP(TimeSpan period)
    {
        var cutoff = DateTime.UtcNow - period;
        lock (_matchLock)
        {
            var inPeriod = _matchHistory.Where(m => m.Timestamp >= cutoff).ToList();
            if (inPeriod.Count == 0) return 0;
            decimal sumPV = inPeriod.Sum(m => m.Price * m.Quantity);
            decimal sumV = inPeriod.Sum(m => m.Quantity);
            return sumV > 0 ? sumPV / sumV : 0;
        }
    }
}
