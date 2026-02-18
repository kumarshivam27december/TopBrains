using System.Collections.Generic;
using System.Linq;

namespace LibraryBookManagementSystem;

public class Catalog<T> where T : Book
{
    private readonly List<T> _items = new List<T>();
    private readonly HashSet<string> _isbnSet = new HashSet<string>();
    private readonly SortedDictionary<string, List<T>> _genreIndex = new SortedDictionary<string, List<T>>();

    public bool AddItem(T item)
    {
        if (_isbnSet.Contains(item.ISBN))
            return false;

        _isbnSet.Add(item.ISBN);
        _items.Add(item);

        if (!_genreIndex.ContainsKey(item.Genre))
            _genreIndex[item.Genre] = new List<T>();
        _genreIndex[item.Genre].Add(item);

        return true;
    }

    public List<T> this[string genre]
    {
        get
        {
            return _genreIndex.TryGetValue(genre, out var list) ? list : new List<T>();
        }
    }

    public IEnumerable<T> FindBooks(Func<T, bool> predicate)
    {
        return _items.Where(predicate);
    }
}
