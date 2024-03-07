using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


public interface ICurrentUserService
{ 
    int? UserId { get; }
    string? UserName { get; } 
   
    Claim? FindClaim(string claimType);
    Claim[] FindClaims(string claimType);
    string? FindClaimValue(string claimType);
}