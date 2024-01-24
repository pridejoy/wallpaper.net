﻿namespace Wallpaper.Net.Common;

/// <summary>
/// 返回统一结果的异常。
/// 需要配合 AppResultActionFilter 使用。
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResultException : Exception
{
    /// <summary>
    /// API 结果
    /// </summary>
    public ApiResult ApiResult { get; private set; }

    public ApiResultException(string error)  : this(new ApiResult())
    {
        if (!string.IsNullOrEmpty(error))
        {
            ApiResult= new ApiResult(500,error);
        }
    }

    public ApiResultException(ApiResult apiResult)  : this(apiResult, null)
    {
    }

    public ApiResultException(Exception innerException)  : base(null, innerException)
    {
        ApiResult = new ApiResult();
    }

    public ApiResultException(ApiResult apiResult, Exception? innerException)  : base(apiResult.Message, innerException)
    {
        ApiResult = apiResult;
    }
}
