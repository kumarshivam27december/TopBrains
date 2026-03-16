using System.Collections.Concurrent;

namespace DistributedCacheWithConsistentHashing;

public class CacheShard<TKey, TValue> where TKey : IEquatable<TKey> where TValue : class
{
    private readonly ConcurrentDictionary<TKey, CacheEntry<TValue>> _data = new();

    public void Set(TKey key, CacheEntry<TValue> entry)
    {
        _data[key] = entry;
    }

    public bool TryGet(TKey key, out CacheEntry<TValue>? entry)
    {
        if (_data.TryGetValue(key, out var e) && (e.ExpiresAt == null || e.ExpiresAt > DateTime.UtcNow))
        {
            entry = e;
            return true;
        }
        entry = null;
        return false;
    }

    public IEnumerable<KeyValuePair<TKey, CacheEntry<TValue>>> GetAll()
    {
        return _data.Where(kv => kv.Value.ExpiresAt == null || kv.Value.ExpiresAt > DateTime.UtcNow);
    }

    public void Remove(TKey key) => _data.TryRemove(key, out _);
}
