using StackExchange.Redis;

namespace ClientSideCaching;

public class ClientSideCache
{
    private readonly IConnectionMultiplexer _mux;

    private Dictionary<string, string> _cache = new();

    public ClientSideCache(IConnectionMultiplexer mux)
    {
        _mux = mux;
    }

    public async ValueTask Set(string key, string value)
    {
        var db = _mux.GetDatabase();
        await db.StringSetAsync(key, value);
        _cache.Remove(key);
        _cache.Add(key, value);
    }

    public async ValueTask<string> Get(string key)
    {
        if (_cache.ContainsKey(key))
        {
            return _cache[key];
        }

        var db = _mux.GetDatabase();
        var result = await db.StringGetAsync(key);
        if (result != RedisValue.Null)
        {
            _cache.Add(key,result);
        }

        return result.ToString();
    }
}