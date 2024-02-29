using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Wallpaper.Net.Common
{
    public static class AuthorizationSetup
    {
        public static IServiceCollection AddAuthorizationSetup(this IServiceCollection services) 
        {

            //添加授权
            services.AddAuthorization();

            services.AddTransient<IAuthorizationPolicyProvider, SimpleAuthorizationPolicyProvider>();
            services.AddTransient<IAuthorizationHandler, SimpleAuthorizationHandler>();
            services.AddTransient<IPermissionChecker, DefaultPermissionChecker>();

            return services;
        }
    }
}
