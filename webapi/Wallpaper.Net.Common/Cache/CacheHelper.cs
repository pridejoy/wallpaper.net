using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Wallpaper.Net.Common;


/// <summary>
/// 自定义缓存帮助类
/// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options">各种设置，如过期时间、滑动过期时间等</param>
    /// <returns></returns>
    public static async Task SetAsync(string key, string value, DistributedCacheEntryOptions options=null)
    {
        var bytes = Encoding.Unicode.GetBytes(value);
        await Cache.SetAsync(key, bytes, options??new DistributedCacheEntryOptions());

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

    public static async Task SetObjectAsync<T>(string key, T value, DistributedCacheEntryOptions options =null)
    {
        var json = JsonConvert.SerializeObject(value);
        var bytes = Encoding.UTF8.GetBytes(json);
        await Cache.SetAsync(key, bytes, options??new DistributedCacheEntryOptions());

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




    public static DistributedCacheEntryOptions GetDefaultCacheEntryOptions()
    {
        // 创建一个新的DistributedCacheEntryOptions实例
        var options = new DistributedCacheEntryOptions();

        // 设置绝对过期时间为1小时后
        //options.SetAbsoluteExpiration(TimeSpan.FromHours(1));

        // 设置滑动过期时间为30分钟
        // 滑动过期时间是指如果在此时间间隔内访问了缓存项，则过期时间将重置
        //options.SetSlidingExpiration(TimeSpan.FromMinutes(30));

        // 返回配置好的缓存项选项
        return options;
    }
}
