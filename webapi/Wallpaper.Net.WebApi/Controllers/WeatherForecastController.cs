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
    /// 啊啊啊
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
        /// 获取天气描述
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecast")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        { 
            return _weatherForecastService.Get();
        }


        /// <summary>
        /// 模拟登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        public string GetToken()
        {  

            // 生成 token
            var dic=new Dictionary<string, string>() {
                { JwtConst.OpenID,"123123"}
            };
            string token = JwtHelper.Create(dic);
            //_httpContext.HttpContext.Response.Headers.Add("access-token", "Bearer "+token);
            // 将 Token 添加到响应头中 
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
            throw new ApiResultException(new ApiResult(401, "测试401返回"));
            throw new Exception("测试异常");
        }

        [HttpGet("Throw2")]
        public string Throw2(string s = "12334")
        {
            throw new Exception("测试异常");
        }


        [HttpGet("Throw3")]
        public string Throw3(string s = "12334")
        {
            throw new ApiResultException("测试异常");
        }
 

        /// <summary>
        /// 测试分布式缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet("cachetest-1")]
        public async Task<string> CacheTest1()
        {
          await CacheHelper.SetAsync("123", "456");
          var a = await CacheHelper.GetAsync("123");
          var b = new Student { name="张三",sex="男" };
          await CacheHelper.SetObjectAsync("z", b);
          var c=await CacheHelper.GetObjectAsync<Student>("z");

          await CacheHelper.RemoveAsync("z");
          var d = await CacheHelper.GetObjectAsync<Student>("z");
            return "ok";
        }

        /// <summary>
        /// 缓存测试类
        /// </summary>
        public class Student { 
        
            public string name { get;set; }
            public string sex { get;set; }
        }
    }
}
