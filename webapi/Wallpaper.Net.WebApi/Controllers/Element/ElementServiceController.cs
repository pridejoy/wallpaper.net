using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wallpaper.Net.Repository.Model;
using Wallpaper.Net.WebApi.Controllers; 

/// <summary>
/// 元素服务
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ElementServiceController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    private readonly GalleryServiceController _galleryService;

    public ElementServiceController(ISqlSugarClient db, GalleryServiceController galleryService)
    {
        _db = db;
        _galleryService = galleryService;
    }


    /// <summary>
    /// 获取每日必应(每日早上八点更新)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<dynamic> CurrDayBing()
    {
        //不建议直接返回数据库字段
        var date = await _db.Queryable<bs_bingwallpaper>()
            .OrderByDescending(x => x.StartDate)
            .Select<EveryDayBingOutput>()
            .FirstAsync();
        return date;
    }

    /// <summary>
    /// 根据日期过去相应的历史必应
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<dynamic> EveryDayBing(BingEveryInput input)
    {
        var date = await _db.Queryable<bs_bingwallpaper>()
           .WhereIF(input.monthstr != null, x => x.StartDate.ToString().Contains(input.monthstr))
           .Where(x => x.MobileUrl != null)
           .OrderBy(x => x.StartDate)
           .Take(35) //最多返回35张
           .ToListAsync();
        return date;
    }

    /// <summary>
    /// 获取指定分类的照片
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<dynamic> GetCategImg()
    {
       return  await _galleryService.RandomGril("RandomGril"); 
    }



    /// <summary>
    /// 获取瀑布流壁纸
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //[HttpGet("waterfall/page")]
    //public async Task<dynamic> WaterfallPage([FromQuery] WaterFallPageInPut input)
    //{
    //    var date = await _db.Queryable<bs_waterfall>()
    //       .Where(ca => ca.Url != null)
    //       .ToPagedListAsync(input.Page, input.PageSize);
    //    date.Items = date.Items.OrderBy(n => Guid.NewGuid());
    //    return date.XnPagedResult();
    //}
}