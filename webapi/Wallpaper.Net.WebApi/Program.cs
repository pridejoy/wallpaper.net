using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Wallpaper.Net.Common;
using Wallpaper.Net.Common.Helper;
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

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();


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

            builder.Services.AddSingleton<MemoryCache>();

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
                app.UseSwaggerUI(c =>
                {
                    //指定Swagger JSON文件的终结点，用于加载和显示API文档。
                    //需要提供JSON文件的URL和一个可识别的名称
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    //指定swagger文档的启动目录 。默认为swagger
                    //可以通过设置为空字符串来让Swagger UI直接在根路径下进行访问
                    //c.RoutePrefix = string.Empty;

                    //设置默认的接口文档展开方式，可选值包括None、List和Full。
                    //默认值为None，表示不展开接口文档；
                    //List表示只展开接口列表；
                    //Full表示展开所有接口详情
                    c.DocExpansion(DocExpansion.None); // 设置为完整模式 
                    c.DisplayRequestDuration();
                    c.EnablePersistAuthorization();
                   
                    //c.UseRequestInterceptor("(request) => { return defaultRequestInterceptor(request); }");
                    //c.UseResponseInterceptor("(response) => { return defaultResponseInterceptor(response); }");

                });
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
