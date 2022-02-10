using System.Collections.Generic;
using System.Security.Claims;
using DraplusApi.Models;
namespace DraplusApi.Helper
{
    public interface IJwtGenerator
    {
        Claim[] GenerateClaims(User user);
        string GenerateJwtToken(IEnumerable<Claim> claims);
    }
}
