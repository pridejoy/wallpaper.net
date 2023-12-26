
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Wallpaper.Net.Common;
using Wallpaper.Net.Common.Jwt;
using Wallpaper.Net.Servers;
using Wallpaper.Net.WebApi.Filter;

namespace Wallpaper.Net.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加静态文件读取
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
              
            // 添加过滤器
            builder.Services.AddControllers(options =>
            {
                // 全局异常过滤
                //options.Filters.Add(typeof(GlobalExceptionsFilter));
                // 日志过滤器
                options.Filters.Add(typeof(RequestActionFilter));
            })
            // 配置Api行为选项
            .ConfigureApiBehaviorOptions(options =>
            {
                // 禁用默认模型验证过滤器
                options.SuppressModelStateInvalidFilter = true;
            })
            // 添加统一返回结果
            .AddApiResult<CustomApiResultProvider>();

            // 配置Json选项
            builder.Services.AddJsonOptions();
             

            // 添加sqlsugar
            builder.Services.AddSqlsugarSetup();

            // 添加swagger文档
            builder.Services.AddSwaggerSetup();

            // 添加基础服务
            builder.Services.AddBaseServicesSetup();

            // 自动添加服务层
            builder.Services.AddAutoServices("Wallpaper.Net.Servers");
              
            // 添加jwt认证
            builder.Services.AddJwtSetup();
            // 添加自定义授权
            builder.Services.AddAuthorizationSetup();

            // 替换默认 PermissionChecker
            builder.Services.Replace(new ServiceDescriptor(typeof(IPermissionChecker), typeof(PermissionChecker), ServiceLifetime.Transient));
             
            // 添加跨域支持
            builder.Services.AddCorsSetup();
            // 添加EndpointsApiExplorer
            builder.Services.AddEndpointsApiExplorer();


            //注册其他依赖
            builder.Services.AddTransient<GalleryServiceController>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
             
            app.UseHttpsRedirection();
            // UseCors 必须在 UseRouting 之后，UseResponseCaching、UseAuthorization 之前
            app.UseCors();

            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization(); 

            //使用静态文件
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
