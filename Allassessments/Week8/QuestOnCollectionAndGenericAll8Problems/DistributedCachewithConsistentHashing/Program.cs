using DistributedCacheWithConsistentHashing;

using (var cache = new DistributedCache<string, string>())
{
    cache.AddNode("node1");
    cache.AddNode("node2");
    await cache.SetAsync("key1", "value1", TimeSpan.FromMinutes(5), 2);
    var result = await cache.GetAsync("key1");
    Console.WriteLine($"Found: {result.Found}, Value: {result.Value}");
}
