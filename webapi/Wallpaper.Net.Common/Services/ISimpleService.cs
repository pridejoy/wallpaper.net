using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

/// <summary>
/// 表示一个简单的服务接口，用于提供访问服务提供程序和 HTTP 上下文的功能。
/// </summary>
public interface ISimpleService
{
    /// <summary>
    /// 获取服务提供程序。
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取 HTTP 上下文。
    /// </summary>
    HttpContext HttpContext { get; }

    /// <summary>
    /// 从服务提供程序获取指定类型的服务实例。
    /// </summary>
    /// <typeparam name="TService">要检索的服务类型。</typeparam>
    /// <returns>如果存在，返回请求的服务实例；否则返回 null。</returns>
    TService GetService<TService>() where TService : class;

    /// <summary>
    /// 从服务提供程序获取指定类型的服务实例。
    /// </summary>
    /// <param name="type">要检索的服务类型。</param>
    /// <returns>如果存在，返回请求的服务实例；否则返回 null。</returns>
    object GetService(Type type);
}