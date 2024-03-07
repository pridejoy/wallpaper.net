using System.Security.Claims; 

namespace Wallpaper.Net.Common.Services;


public class CurrentUserService : ICurrentUserService
{
    protected readonly ISimpleService _simpleService;

    public CurrentUserService(ISimpleService simpleService)
    {
        _simpleService = simpleService;
    }

    public virtual ClaimsPrincipal User
    {
        get
        {
            // 如果 Identity 为 null 则抛出异常
            if (_simpleService.HttpContext.User.Identity == null)
            {
                throw new Exception("不能获取到当前用户的状态！");
            }
            return _simpleService.HttpContext.User;
        }
    }

    public virtual bool IsAuthenticated => User.Identity!.IsAuthenticated;

    public virtual string? UserName => FindClaimValue(JwtConst.UserName); // User.Identity!.Name;

 

    public virtual int? UserId
    {
        get
        {
            if (int.TryParse(FindClaimValue(JwtConst.UserId), out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }

     
     

    public virtual Claim? FindClaim(string claimType)
    {
        return User.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public virtual Claim[] FindClaims(string claimType)
    {
        return User.Claims.Where(c => c.Type == claimType).ToArray();
    }

    public virtual string? FindClaimValue(string claimType)
    {
        return FindClaim(claimType)?.Value;
    }
}
