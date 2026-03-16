namespace DistributedCacheWithConsistentHashing;

public class CacheResult<T> where T : class
{
    public bool Found { get; set; }
    public T? Value { get; set; }
    public long Version { get; set; }
}
