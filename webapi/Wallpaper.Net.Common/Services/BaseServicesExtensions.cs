﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.Net.Common.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class BaseServicesExtensions
{
    /// <summary>
    /// 注册基础服务（当前用户等）
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaseServicesSetup(this IServiceCollection services)
    {

        // 注册 IHttpContextAccessor
        services.AddHttpContextAccessor();
        //服务基类
        services.AddScoped<ISimpleService, SimpleService>();
        ///注册基础服务（当前用户等）
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
