namespace TDDLesson;

public sealed class HybridCache<TKey, TValue>
{
    private readonly IMemoryCache<TKey, TValue> _memoryCache;
    private readonly IRedisCache<TKey, TValue> _redisCache;
    private readonly IRepository<TKey, TValue> _repository;
    private readonly TimeSpan _defaultCacheDuration;

    public HybridCache(
        IMemoryCache<TKey, TValue> memoryCache,
        IRedisCache<TKey, TValue> redisCache,
        IRepository<TKey, TValue> repository,
        TimeSpan defaultCacheDuration)
    {
        _memoryCache = memoryCache;
        _redisCache = redisCache;
        _repository = repository;
        _defaultCacheDuration = defaultCacheDuration;
    }

    public async Task<TValue?> GetOrAddAsync(TKey key)
    {
        TValue cacheValue = default;
        var cachedValueFromMemoryCache = await _memoryCache.TryGetAsync(key);
        if (cachedValueFromMemoryCache is null)
        {
            var cachedValueFromRedisCache = await _redisCache.TryGetAsync(key);
            if (cachedValueFromRedisCache is null)
            {
                var valueFromDatabase = await _repository.TryGetAsync(key);
                if (valueFromDatabase is null)
                    return default;

                cacheValue = valueFromDatabase;
                await _memoryCache.AddAsync(key, cacheValue);
                await _redisCache.AddAsync(key, cacheValue);
            }
            else
            {
                cacheValue = cachedValueFromRedisCache;
                await _memoryCache.AddAsync(key, cacheValue);   
            }
        }
        else
        {
            cacheValue = cachedValueFromMemoryCache;
        }

        return cacheValue;
    }
}

public interface IRedisCache<TKey, TValue>
{
    public Task<TValue?> TryGetAsync(TKey key);
    
    public Task AddAsync(TKey key, TValue value);
}

public interface IMemoryCache<TKey, TValue>
{
    public Task<TValue?> TryGetAsync(TKey key);
    
    public Task AddAsync(TKey key, TValue value);
}

public interface IRepository<TKey, TValue>
{
    public Task<TValue?> TryGetAsync(TKey key);
}