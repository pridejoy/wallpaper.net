using Dm.filter.log;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Wallpaper.Net.Common;
using SqlSugar.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Wallpaper.Net.Servers.WeatherForecast;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace Wallpaper.Net.WebApi.Controllers
{
    /// <summary>
    /// ������
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        //private readonly MemoryCache _memorecache;
        private readonly WeatherForecastService _weatherForecastService;
        private  readonly IHttpContextAccessor _httpContext;
        private readonly IDistributedCache _cache;
        public WeatherForecastController(WeatherForecastService weatherForecastService ,
            IDistributedCache cache ,
            IHttpContextAccessor httpContextAccessor)
        {
            _weatherForecastService = weatherForecastService;
            _httpContext = httpContextAccessor; 
            _cache = cache;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecast")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        { 
            return _weatherForecastService.Get();
        }


        /// <summary>
        /// ģ���¼
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        public string GetToken()
        {  

            // ���� token
            var dic=new Dictionary<string, string>() {
                { JwtConst.OpenID,"123123"}
            };
            string token = JwtHelper.Create(dic);
            //_httpContext.HttpContext.Response.Headers.Add("access-token", "Bearer "+token);
            // �� Token ��ӵ���Ӧͷ�� 
            return token; 
        }


        [HttpGet("error")] 
        public string error()
        {

            throw new Exception("12312");
            return "";
        }




        [HttpGet("Return401")]
        public string Throw()
        {
            throw new ApiResultException(new ApiResult(401, "����401����"));
            throw new Exception("�����쳣");
        }

        [HttpGet("Throw2")]
        public string Throw2(string s = "12334")
        {
            throw new Exception("�����쳣");
        }


        [HttpGet("Throw3")]
        public string Throw3(string s = "12334")
        {
            throw new ApiResultException("�����쳣");
        }
 

        /// <summary>
        /// ���Էֲ�ʽ����
        /// </summary>
        /// <returns></returns>
        [HttpGet("cachetest-1")]
        public async Task<string> CacheTest1()
        {
          await CacheHelper.SetAsync("123", "456");
          var a = await CacheHelper.GetAsync("123");
          var b = new Student { name="����",sex="��" };
          await CacheHelper.SetObjectAsync("z", b);
          var c=await CacheHelper.GetObjectAsync<Student>("z");

          await CacheHelper.RemoveAsync("z");
          var d = await CacheHelper.GetObjectAsync<Student>("z");
            return "ok";
        }

        /// <summary>
        /// ���������
        /// </summary>
        public class Student { 
        
            public string name { get;set; }
            public string sex { get;set; }
        }
    }
}
