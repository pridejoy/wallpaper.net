using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wallpaper.Net.Common;
using Wallpaper.Net.Servers;

namespace Wallpaper.Net.WebApi.Controllers
{
    /// <summary>
    /// 小程序基本服务
    /// </summary>
   
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController : ControllerBase
    {
        private readonly ISqlSugarClient _db;
        private readonly WechatService _wechatService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ICurrentUserService _currentUserService;
        
        public WechatController(ISqlSugarClient db, WechatService wechatService, 
            IHttpContextAccessor httpContext ,ICurrentUserService currentUserService)
        {
            _db = db;
            _wechatService = wechatService;
            _httpContext = httpContext;
            _currentUserService = currentUserService;
        }


        /// <summary>
        /// 静默授权登录接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<dynamic> LoginAsync([FromBody] WechatPhoneLoginInput input)
        {
            var customerinfo = await _wechatService.GetOpenID(input.Code);
            var customerentity = await _db.Queryable<bs_customer>().Where(x => x.OpenID == customerinfo.openid).FirstAsync();
            if (customerentity == null)
            {
                customerentity = new bs_customer();
                customerentity.OpenID = customerinfo.openid;
                customerentity.NickName = "微信用户";
                customerentity.AvatarUrl = "http://imagesoss.hunji.xyz/conch-net/avatar/1.png";
                customerentity.CreatedDate = DateTime.Now;
                customerentity.UserID = _db.Storageable(customerentity).ExecuteReturnEntity().UserID;
            }

            //生成Token
            var jwtmodel = new JwtTokenModel(userId:customerentity.UserID,userName:customerentity.OpenID) ;
            // 生成刷新Token令牌
            var token = JwtHelper.Create(jwtmodel);
            // 设置Swagger自动登录
            //httpContext.Response.Headers["access-token"] = accessToken;

            return new WxLoginOutput { token = token, userinfo = customerentity };
        }

        /// <summary>
        /// 用户基本信息输出
        /// </summary>
        /// <returns></returns>
        [HttpGet("getinfo")]
        [Authorize]
        public async Task<dynamic> getInfo()
        {
            var openid = _currentUserService.UserName;
            if (openid == null)  throw new ApiResultException("获取用户信息失败，请重新登录");
            var user = await _db.Queryable<bs_customer>()
                .Where(x => x.OpenID == openid)
                .FirstAsync();
            if (user != null) return user;
            throw new ApiResultException("获取用户信息失败，请重新登录"); 
        }

        /// <summary>
        /// 模拟的登录
        /// </summary>
        /// <param name="Password"></param>
        /// <returns>LoginOutput</returns>
        /// <example>Keltsing@123</example>
        [HttpPost("Testlogin")]
        public async Task<WxLoginOutput> Login(string Password= "os0uq5TUMheUQ_IIdEG4fVJ4ORNo")
        {
            var customerentity = await _db.Queryable<bs_customer>()
                .Where(x => x.OpenID == Password).FirstAsync();
            if (customerentity == null)
            {
                throw new ApiResultException("获取用户信息失败，请重新登录");
            }


            //生成Token
            var jwtmodel = new JwtTokenModel(userId: customerentity.UserID, userName: customerentity.OpenID);
            // 生成刷新Token令牌
            var token = JwtHelper.Create(jwtmodel);

            // 设置Swagger自动登录
            _httpContext.HttpContext.Response.Headers["access-token"] = token;

            return new WxLoginOutput { token = token, userinfo = customerentity };
        }

    }
}
