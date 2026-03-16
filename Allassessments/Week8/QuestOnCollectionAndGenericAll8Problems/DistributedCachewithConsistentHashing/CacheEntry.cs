namespace DistributedCacheWithConsistentHashing;

public class CacheEntry<T> where T : class
{
    public T Value { get; set; } = null!;
    public DateTime? ExpiresAt { get; set; }
    public long Version { get; set; }
}
