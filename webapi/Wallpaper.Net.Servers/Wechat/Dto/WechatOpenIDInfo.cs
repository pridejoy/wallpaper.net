using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper.Net.Servers
{
    /// <summary>
    /// 微信小程序登录
    /// </summary>
    public class WechatOpenIDInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errmsg { get; set; }
    }
}
