using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TraineeTrack.API.Models.DB;

namespace TraineeTrack.API.Services;

public class JwtService : IJwtService
{
    private readonly SymmetricSecurityKey _signingKey;
    private readonly SigningCredentials _signingCredentials;

    public JwtService(IConfiguration configuration)
    {
        var key = new byte[32];
        Convert.TryFromBase64String(configuration["JWT_SECRET"] ?? "", new Span<byte>(key), out _);

        _signingKey = new SymmetricSecurityKey(key);
        _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
    }

    private const string Issuer = "TraineeTrackAPI";
    private const string Audience = "TraineeTrackClient";

    public TokenValidationParameters ValidationParameters =>
        new()
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,

            ValidateAudience = true,
            ValidAudience = Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingKey
        };

    public string GenerateJwtToken(User user)
    {
        var jwt = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims:
            [
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            ],
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: _signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public User this[string token]
    {
        get
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _signingKey;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal =
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            var userIdClaim = principal.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);

            return new User
            {
                Id = Guid.Parse(userIdClaim.Value)
            };
        }
    }
}