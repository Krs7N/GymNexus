using GymNexus.Core.Contracts;
using GymNexus.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymNexus.Core.Utils;

namespace GymNexus.Core.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateJwtToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>();

        if (user.FirstName != null && user.LastName != null)
        {
            claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };
        }

        claims.AddRange(new List<Claim>()
        {   
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        });

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[JwtConfigs.Key]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        _config[JwtConfigs.Issuer],
        _config[JwtConfigs.Audience],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}