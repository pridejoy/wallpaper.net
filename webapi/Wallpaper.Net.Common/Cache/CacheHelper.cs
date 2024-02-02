using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Wallpaper.Net.Common;



public static class CacheHelper
{
    private static IDistributedCache? _cache;

    private static IDistributedCache Cache
    {
        get
        {
            if (_cache == null) throw new NullReferenceException(nameof(Cache));
            return _cache;
        }
    }

    public const string KeySetCacheKey = "key_set";

    public static void Configure(IDistributedCache? cache)
    {
        if (_cache != null)
        {
            throw new Exception($"{nameof(Cache)}不可修改！");
        }
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    #region 缓存操作方法

    public static async Task<string?> GetAsync(string key)
    {
        var data = await Cache.GetAsync(key);
        return data != null ? Encoding.Unicode.GetString(data) : null;
    }

    public static async Task SetAsync(string key, string value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.Unicode.GetBytes(value);
        await Cache.SetAsync(key, bytes, options);

        // 更新键集
        await UpdateKeySetAsync(key);
    }

    public static async Task<T?> GetObjectAsync<T>(string key)
    {
        var data = await Cache.GetAsync(key);
        if (data == null) return default;

        var json = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static async Task SetObjectAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        var json = JsonConvert.SerializeObject(value);
        var bytes = Encoding.UTF8.GetBytes(json);
        await Cache.SetAsync(key, bytes, options);

        // 更新键集
        await UpdateKeySetAsync(key);
    }

    private static async Task UpdateKeySetAsync(string key)
    {
        var keySet = await GetObjectAsync<HashSet<string>>(KeySetCacheKey) ?? new HashSet<string>();
        keySet.Add(key);
        await SetObjectAsync(KeySetCacheKey, keySet, new DistributedCacheEntryOptions());
    }

    public static async Task RemoveAsync(string key)
    {
        await Cache.RemoveAsync(key);

        // 更新键集
        var keySet = await GetObjectAsync<HashSet<string>>(KeySetCacheKey);
        if (keySet != null && keySet.Remove(key))
        {
            await SetObjectAsync(KeySetCacheKey, keySet, new DistributedCacheEntryOptions());
        }
    }


    /// <summary>
    /// 异步获取所有缓存中的键。
    /// </summary>
    /// <returns>包含所有缓存键的集合。</returns>
    public static async Task<HashSet<string>?> GetAllKeysAsync()
    {
        return await GetObjectAsync<HashSet<string>>(KeySetCacheKey);
    }

    /// <summary>
    /// 获取指定字段的key
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static async Task<IEnumerable<string>> GetKeysByPrefixAsync(string prefix)
    {
        var allKeys = await GetAllKeysAsync();
        if (allKeys == null) return Enumerable.Empty<string>();

        // 使用LINQ查询来筛选具有指定前缀的键
        var filteredKeys = allKeys.Where(key => key.StartsWith(prefix));
        return filteredKeys;
    }
    #endregion
}
