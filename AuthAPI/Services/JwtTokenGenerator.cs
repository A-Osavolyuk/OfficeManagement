using AuthAPI.Models;
using AuthAPI.Models.Entities;
using AuthAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions options;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    public string GenerateTokenAsync(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(options.Secret);

        var claimsList = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = options.Audience,
            Issuer = options.Issuer,
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
