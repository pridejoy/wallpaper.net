using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Wallpaper.Net.Servers.WeatherForecast
{
    public class WeatherForecastService
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
         
        readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// 查询天气信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeatherForecast> Get()
        {
            var MyList = Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            return MyList;
        }
    }
}
