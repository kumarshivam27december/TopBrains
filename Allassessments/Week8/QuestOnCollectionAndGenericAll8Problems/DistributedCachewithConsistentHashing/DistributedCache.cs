using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DistributedCacheWithConsistentHashing;

public class DistributedCache<TKey, TValue> : IDisposable
    where TKey : IEquatable<TKey>
    where TValue : class
{
    private readonly SortedDictionary<uint, CacheNode> _hashRing = new();
    private readonly ConcurrentDictionary<TKey, CacheEntry<TValue>> _localCache = new();
    private readonly ReaderWriterLockSlim _ringLock = new(LockRecursionPolicy.SupportsRecursion);
    private const int VirtualNodesPerPhysicalNode = 200;
    private bool _disposed;

    private class CacheNode
    {
        public string NodeId { get; }
        public uint HashPosition { get; }
        public CacheShard<TKey, TValue> Shard { get; } = new();

        public CacheNode(string nodeId, uint hashPosition)
        {
            NodeId = nodeId;
            HashPosition = hashPosition;
        }
    }

    private class RingPosition
    {
        public string NodeId { get; set; } = string.Empty;
        public uint Hash { get; set; }
    }

    public void AddNode(string nodeId)
    {
        _ringLock.EnterWriteLock();
        try
        {
            for (int i = 0; i < VirtualNodesPerPhysicalNode; i++)
            {
                var pos = Hash($"{nodeId}:{i}");
                _hashRing[pos] = new CacheNode(nodeId, pos);
            }
        }
        finally { _ringLock.ExitWriteLock(); }
    }

    public void RemoveNode(string nodeId)
    {
        _ringLock.EnterWriteLock();
        try
        {
            var toRemove = _hashRing.Where(kv => kv.Value.NodeId == nodeId).Select(kv => kv.Key).ToList();
            foreach (var k in toRemove) _hashRing.Remove(k);
        }
        finally { _ringLock.ExitWriteLock(); }
    }

    private static uint Hash(string key)
    {
        uint hash = 0;
        foreach (char c in key)
            hash = ((hash << 5) + hash) + c;
        return hash;
    }

    private uint HashKey(TKey key)
    {
        return Hash(key?.ToString() ?? "");
    }

    private CacheNode? GetNodeForKey(uint keyHash)
    {
        _ringLock.EnterReadLock();
        try
        {
            foreach (var kv in _hashRing)
                if (kv.Key >= keyHash) return kv.Value;
            return _hashRing.Values.FirstOrDefault();
        }
        finally { _ringLock.ExitReadLock(); }
    }

    public async Task<bool> SetAsync(TKey key, TValue value, TimeSpan? ttl = null, int replicationFactor = 3)
    {
        var keyHash = HashKey(key);
        var entry = new CacheEntry<TValue>
        {
            Value = value,
            ExpiresAt = ttl.HasValue ? DateTime.UtcNow + ttl.Value : null,
            Version = DateTime.UtcNow.Ticks
        };
        _localCache[key] = entry;

        _ringLock.EnterReadLock();
        try
        {
            var nodes = _hashRing.Values.DistinctBy(n => n.NodeId).Take(replicationFactor).ToList();
            foreach (var node in nodes)
            {
                node.Shard.Set(key, entry);
            }
        }
        finally { _ringLock.ExitReadLock(); }

        await Task.CompletedTask;
        return true;
    }

    public async Task<CacheResult<TValue>> GetAsync(TKey key, bool readRepair = false)
    {
        if (_localCache.TryGetValue(key, out var entry) && (entry.ExpiresAt == null || entry.ExpiresAt > DateTime.UtcNow))
        {
            return await Task.FromResult(new CacheResult<TValue> { Found = true, Value = entry.Value, Version = entry.Version });
        }
        var keyHash = HashKey(key);
        var node = GetNodeForKey(keyHash);
        if (node != null && node.Shard.TryGet(key, out var shardEntry))
        {
            _localCache[key] = shardEntry!;
            return await Task.FromResult(new CacheResult<TValue> { Found = true, Value = shardEntry!.Value, Version = shardEntry.Version });
        }
        return await Task.FromResult(new CacheResult<TValue> { Found = false });
    }

    public async Task RebalanceRingAsync()
    {
        _ringLock.EnterReadLock();
        try
        {
            await Task.Yield();
        }
        finally { _ringLock.ExitReadLock(); }
    }

    public async IAsyncEnumerable<KeyValuePair<TKey, TValue>> QueryAsync(
        Expression<Func<KeyValuePair<TKey, TValue>, bool>> predicate,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var compiled = predicate.Compile();
        _ringLock.EnterReadLock();
        try
        {
            foreach (var node in _hashRing.Values.DistinctBy(n => n.NodeId))
            {
                foreach (var kv in node.Shard.GetAll())
                {
                    if (cancellationToken.IsCancellationRequested) yield break;
                    var pair = new KeyValuePair<TKey, TValue>(kv.Key, kv.Value.Value);
                    if (compiled(pair))
                        yield return pair;
                }
            }
        }
        finally { _ringLock.ExitReadLock(); }
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _ringLock.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
