using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Wallpaper.Net.Common
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        { 

            // 默认配置
            Action<SwaggerGenOptions> defaultSetupAction = options =>
            {
                var basePath = AppContext.BaseDirectory;

                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "在线接口文档",
                        Version = "v1"
                    });

                // 获取根目录下，所有 xml 完整路径（注：并不会获取二级目录下的文件）
                var directoryInfo = new DirectoryInfo(basePath);
                List<string> xmls = directoryInfo
                    .GetFiles()
                    .Where(f => f.Name.ToLower().EndsWith(".xml"))
                    .Select(f => f.FullName)
                    .ToList();

                // 添加注释文档
                foreach (var xml in xmls)
                {
                    options.IncludeXmlComments(xml,true);
                }

                // api有"Authorize" 特性，加上小锁
                options.OperationFilter<AuthenticationOperationFilter>();

                options.OperationFilter<SecurityRequirementsOperationFilter>(); 

                // 接入 Jwt 认证
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "在下面输入框输入Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });


            };

            // 注册 Swagger 并添加默认配置
            services.AddSwaggerGen(defaultSetupAction);

            // 如果有自定义配置
            //if (setupAction != null) services.Configure(setupAction);

            return services;
        }

    }
}
