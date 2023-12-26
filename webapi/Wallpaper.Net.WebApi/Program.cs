
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

            // ���Ӿ�̬�ļ���ȡ
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
              
            // ���ӹ�����
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
            // ����ͳһ���ؽ��
            .AddApiResult<CustomApiResultProvider>();

            // ����Jsonѡ��
            builder.Services.AddJsonOptions();
             

            // ����sqlsugar
            builder.Services.AddSqlsugarSetup();

            // ����swagger�ĵ�
            builder.Services.AddSwaggerSetup();

            // ���ӻ�������
            builder.Services.AddBaseServicesSetup();

            // �Զ����ӷ����
            builder.Services.AddAutoServices("Wallpaper.Net.Servers");
              
            // ����jwt��֤
            builder.Services.AddJwtSetup();
            // �����Զ�����Ȩ
            builder.Services.AddAuthorizationSetup();

            // �滻Ĭ�� PermissionChecker
            builder.Services.Replace(new ServiceDescriptor(typeof(IPermissionChecker), typeof(PermissionChecker), ServiceLifetime.Transient));
             
            // ���ӿ���֧��
            builder.Services.AddCorsSetup();
            // ����EndpointsApiExplorer
            builder.Services.AddEndpointsApiExplorer();


            //ע����������
            builder.Services.AddTransient<GalleryServiceController>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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