using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper.Net.Common
{

    public class JwtTokenModel
    {
        public int UserId { get;  set; }
        public string UserName { get;  set; }
        public string[] Roles { get;  set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public int Expiration { get; set; } = 1800;

        public JwtTokenModel(int userId, string userName, params string[] roles)
        {
            UserId = userId;
            UserName = userName;
            Roles = roles;
        }
         

        public JwtTokenModel(int userId, string userName, List<Claim> claims, params string[] roles)
            : this(userId,userName, roles)
        {
            Claims = claims;
        }

        public JwtTokenModel(int userId, string userName, List<Claim> claims, int expiration, params string[] roles)
            : this(userId,userName, claims, roles)
        {
            Expiration = expiration;
        }
    }
}
