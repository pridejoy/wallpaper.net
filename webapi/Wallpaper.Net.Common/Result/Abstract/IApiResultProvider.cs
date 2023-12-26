using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallpaper.Net.Common;



public interface IApiResultProvider
{
    // 处理 IActionResult 对象的方法
    IActionResult ProcessActionResult(IActionResult actionResult);

    // 处理 ApiResultException 异常的方法
    IActionResult ProcessApiResultException(ApiResultException resultException);
}
