using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper.Net.Servers.Wechat
{
    internal interface IWechatService
    {
        /// <summary>
        /// 获取微信登录授权的openid
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<WechatOpenIDInfo> GetOpenID(string code);
    }
}
