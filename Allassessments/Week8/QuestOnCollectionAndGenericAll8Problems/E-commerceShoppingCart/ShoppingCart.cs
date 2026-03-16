using System.Collections.Generic;
using System.Linq;

namespace EcommerceShoppingCart;

public class ShoppingCart<T> where T : Product
{
    private readonly Dictionary<T, int> _cartItems = new Dictionary<T, int>();

    public void AddToCart(T product, int quantity)
    {
        if (_cartItems.TryGetValue(product, out var existing))
            _cartItems[product] = existing + quantity;
        else
            _cartItems[product] = quantity;
    }

    public double CalculateTotal(Func<T, double, double>? discountCalculator = null)
    {
        double total = 0;
        foreach (var item in _cartItems)
        {
            double price = item.Key.Price * item.Value;
            if (discountCalculator != null)
                price = discountCalculator(item.Key, price);
            total += price;
        }
        return total;
    }

    public List<T> GetTopExpensiveItems(int n)
    {
        return _cartItems.Keys
            .OrderByDescending(p => p.Price)
            .Take(n)
            .ToList();
    }
}
