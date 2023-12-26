using Microsoft.AspNetCore.Http;

namespace Wallpaper.Net.Common.Services;



public class SimpleService : ISimpleService
{
    // 用于缓存已经获取的服务对象
    protected IDictionary<Type, object> CacheServices { get; set; } = new Dictionary<Type, object>();

    // 构造函数，接收一个 IServiceProvider 实例作为参数
    public SimpleService(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;

        // 不要在构造函数中捕获 IHttpContextAccessor.HttpContext。
        // 详见：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-context?view=aspnetcore-6.0#httpcontext-isnt-thread-safe
        //HttpContext = accessor.HttpContext ?? throw new ArgumentNullException(nameof(HttpContext));
    }

    // 获取服务提供程序实例
    public virtual IServiceProvider ServiceProvider { get; private set; }

    // 获取 IHttpContextAccessor 实例
    public virtual IHttpContextAccessor HttpContextAccessor => GetService<IHttpContextAccessor>();

    // 获取 HTTP 上下文
    public virtual HttpContext HttpContext => HttpContextAccessor.HttpContext!;

    // 获取指定类型的服务实例
    public virtual TService GetService<TService>()
        where TService : class
    {
        var service = GetService(typeof(TService)) as TService;
        if (service == null)
        {
            throw new NullReferenceException(nameof(service));
        }
        return service;
    }

    // 获取指定类型的服务实例
    public virtual object GetService(Type type)
    {
        // 如果已经缓存了该类型的服务实例，则直接返回
        if (CacheServices.TryGetValue(type, out var obj))
        {
            return obj;
        }

        // 否则从服务提供程序中获取服务实例
        var service = ServiceProvider.GetService(type);
        if (service == null)
        {
            throw new NullReferenceException(nameof(service));
        }

        // 将获取到的服务实例缓存起来
        return CacheServices[type] = service;
    }
}
