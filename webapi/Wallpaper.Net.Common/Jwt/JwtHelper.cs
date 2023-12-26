using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SqlSugar.Extensions;

namespace Wallpaper.Net.Common
{
    public static class JwtHelper
    {
        /// <summary>
        /// 生成 JWT Token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string Create(JwtTokenModel tokenModel)
        {
            // 获取配置
            string issuer = AppSettings.Jwt.Issuer;
            string audience = AppSettings.Jwt.Audience;
            string secret = AppSettings.Jwt.SecretKey;


            var claims = new List<Claim>
              {
                 /*
                 * 特别重要：
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                   2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                 */                

                new Claim(SimpleClaimTypes.UserId, tokenModel.UserId.ToString()),
                new Claim(SimpleClaimTypes.UserName, tokenModel.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (SimpleClaimTypes.Expiration,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(SimpleClaimTypes.Issuer,issuer),
                new Claim(SimpleClaimTypes.Audience,audience), 

               };
             

            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            claims.AddRange(tokenModel.Roles.Select(s => new Claim(ClaimTypes.Role, s)));


            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }


        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JwtTokenModel DeserializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtTokenModel tokenModelJwt = new JwtTokenModel(-1, "");

            if (!string.IsNullOrEmpty(jwtStr) && jwtHandler.CanReadToken(jwtStr))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

                // 获取用户ID
                if (jwtToken.Claims.FirstOrDefault(c => c.Type == SimpleClaimTypes.UserId) != null)
                {
                    tokenModelJwt.UserId = Convert.ToInt32(jwtToken.Claims.First(c => c.Type == SimpleClaimTypes.UserId).Value);
                }

                // 获取用户名
                if (jwtToken.Claims.FirstOrDefault(c => c.Type == SimpleClaimTypes.UserName) != null)
                {
                    tokenModelJwt.UserName = jwtToken.Claims.First(c => c.Type == SimpleClaimTypes.UserName).Value;
                }

                // 获取角色
                if (jwtToken.Claims.FirstOrDefault(c => c.Type == SimpleClaimTypes.Role) != null)
                {
                    tokenModelJwt.Roles = jwtToken.Claims.Where(c => c.Type == SimpleClaimTypes.Role).Select(c => c.Value).ToArray();
                }

                // 获取过期时间
                if (jwtToken.ValidTo != null)
                {
                    tokenModelJwt.Expiration = (int)jwtToken.ValidTo.Subtract(DateTime.UtcNow).TotalSeconds;
                }
            }

            return tokenModelJwt;
        }
    }
}
