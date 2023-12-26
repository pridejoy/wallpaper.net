﻿using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wallpaper.Net.Repository.Model;

/// <summary>
/// 小程序服务
/// </summary>

[ApiController]
[Route("api/[controller]/[action]")]
public class MiniServiceController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public MiniServiceController(ISqlSugarClient db)
    {
        _db = db;
    }


    /// <summary>
    /// 获取小程序的友联
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<dynamic> SiteLink()
    {  
        var list = await _db.Queryable<bs_miniappsitelink>()
            .Where(x => x.IsDeleted == false).ToListAsync(); 
        //list.Add(new MiniAppSiteLinkOutPut { App_id = "wxf3d81a452b88ff4b", Url = "http://imagesoss.hunji.xyz/tuniao/sitelink/199750c10f5f66cf296ee68be38ca81a.jpg", Title = "图鸟UI" });
         
        return list;
    }





}