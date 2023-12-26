using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Wallpaper.Net.Repository.Model;


[SugarTable("bs_bingwallpaper")]
public class bs_bingwallpaper
{
    [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
    public int Id { get; set; }

    public string CopyRight { get; set; }

    public string GitUrl { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartDate { get; set; }

    public string Url { get; set; }

    public string MobileUrl { get; set; }

    public bool IsDeleted { get; set; } = false;
}
