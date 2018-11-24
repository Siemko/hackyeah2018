using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Orlen.Services.UserService.Models;

namespace Orlen.API.Authorization
{
    public static class TokenHelper
    {
        public static TokenModel GenerateTokenForUser(UserModel user)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset) now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var expiryDate = now.Add(GetAccessTokenExpirationTime());

            var accessJwt = new JwtSecurityToken(GetIssuer(),
                                                 GetAudience(),
                                                 claims,
                                                 now,
                                                 expiryDate,
                                                 new SigningCredentials(GetSignInKey(), SecurityAlgorithms.HmacSha256));

            var encodedAccessJwt = new JwtSecurityTokenHandler().WriteToken(accessJwt);


            var refreshJwt = new JwtSecurityToken(GetIssuer(),
                                                  GetAudience(),
                                                  claims,
                                                  now,
                                                  now.Add(GetRefreshTokenExpirationTime()),
                                                  new SigningCredentials(GetSignInKey(), SecurityAlgorithms.HmacSha256));

            new JwtSecurityTokenHandler().WriteToken(refreshJwt);

            return new TokenModel
            {
                Token = encodedAccessJwt,
                Email = user.Email,
                ExpiryDate = expiryDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                Role = user.Role,
            };
        }

        public static TokenValidationParameters GetJwtBearerOptions()
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSignInKey(),
                ValidateIssuer = true,
                ValidIssuer = GetIssuer(),
                ValidateAudience = true,
                ValidAudience = GetAudience(),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenValidationParameters;
        }

        private static SymmetricSecurityKey GetSignInKey()
        {
            const string secretKey = "2gh3h2ndklhdfaskfbkjbasdjbk12130asdn1nampqoiwe912";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            return signingKey;
        }

        private static string GetIssuer()
        {
            return "issuer";
        }

        private static string GetAudience()
        {
            return "audience";
        }

        private static TimeSpan GetAccessTokenExpirationTime()
        {
            return TimeSpan.FromHours(12);
        }

        private static TimeSpan GetRefreshTokenExpirationTime()
        {
            return TimeSpan.FromHours(24);
        }
    }
}