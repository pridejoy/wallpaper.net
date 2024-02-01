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


            // ��Ӿ�̬�ļ���ȡ
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
              
            // ��ӹ�����
            builder.Services.AddControllers(options =>
            {
                // ȫ���쳣����
                //options.Filters.Add(typeof(GlobalExceptionsFilter));
                // ��־������
                options.Filters.Add(typeof(RequestActionFilter));
            })
            // ����Api��Ϊѡ��
            .ConfigureApiBehaviorOptions(options =>
            {
                // ����Ĭ��ģ����֤������
                options.SuppressModelStateInvalidFilter = true;
            })
            // ���ͳһ���ؽ��
            .AddApiResult<CustomApiResultProvider>();

            // ����Jsonѡ��
            builder.Services.AddJsonOptions();

            builder.Services.AddSingleton<MemoryCache>();

            // ���sqlsugar
            builder.Services.AddSqlsugarSetup();

            // ���swagger�ĵ�
            builder.Services.AddSwaggerSetup();

            // ��ӻ�������
            builder.Services.AddBaseServicesSetup();

            // �Զ���ӷ����
            builder.Services.AddAutoServices("Wallpaper.Net.Servers");
              
            // ���jwt��֤
            builder.Services.AddJwtSetup();
            // ����Զ�����Ȩ
            builder.Services.AddAuthorizationSetup();

            // �滻Ĭ�� PermissionChecker
            builder.Services.Replace(new ServiceDescriptor(typeof(IPermissionChecker), typeof(PermissionChecker), ServiceLifetime.Transient));
             
            // ��ӿ���֧��
            builder.Services.AddCorsSetup();
            // ���EndpointsApiExplorer
            builder.Services.AddEndpointsApiExplorer();


            //ע����������
            builder.Services.AddTransient<GalleryServiceController>();

            var app = builder.Build();

             

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    //ָ��Swagger JSON�ļ����ս�㣬���ڼ��غ���ʾAPI�ĵ���
                    //��Ҫ�ṩJSON�ļ���URL��һ����ʶ�������
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    //ָ��swagger�ĵ�������Ŀ¼ ��Ĭ��Ϊswagger
                    //����ͨ������Ϊ���ַ�������Swagger UIֱ���ڸ�·���½��з���
                    //c.RoutePrefix = string.Empty;

                    //����Ĭ�ϵĽӿ��ĵ�չ����ʽ����ѡֵ����None��List��Full��
                    //Ĭ��ֵΪNone����ʾ��չ���ӿ��ĵ���
                    //List��ʾֻչ���ӿ��б�
                    //Full��ʾչ�����нӿ�����
                    c.DocExpansion(DocExpansion.None); // ����Ϊ����ģʽ 
                    c.DisplayRequestDuration();
                    c.EnablePersistAuthorization();
                   
                    //c.UseRequestInterceptor("(request) => { return defaultRequestInterceptor(request); }");
                    //c.UseResponseInterceptor("(response) => { return defaultResponseInterceptor(response); }");

                });
            }
             
            app.UseHttpsRedirection();
            // UseCors ������ UseRouting ֮��UseResponseCaching��UseAuthorization ֮ǰ
            app.UseCors();

            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization(); 

            //ʹ�þ�̬�ļ�
            app.UseStaticFiles();

            app.MapControllers();

            app.Run(); 
        }
    }
}
