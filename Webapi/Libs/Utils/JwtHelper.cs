using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Webapi.Libs.Jwt {
    public class JwtHelper {
        private readonly string _jwtSecret;
        private readonly string _keyId;

        public JwtHelper(string jwtSecret, string keyId) {
            _jwtSecret = jwtSecret;
            _keyId = keyId;
        }

        public virtual string GenerateJwtToken(Models.UserModel user) {
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials) {
                { "kid", _keyId }
            };

            var scope = new Dictionary<string, string> {
                { "first", "yes" },
                { "second", "no" }
            };

            var scopeJson = JsonSerializer.Serialize(scope);

            var now = DateTimeOffset.UtcNow;
            var payload = new JwtPayload {
                { JwtRegisteredClaimNames.Sub, user.Id.ToString() },
                { JwtRegisteredClaimNames.Email, user.Email },
                { "scope", JsonDocument.Parse(scopeJson).RootElement },
                { JwtRegisteredClaimNames.Nbf, now.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, now.AddHours(1).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds() }
            };

            var securityToken = new JwtSecurityToken(header, payload);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public virtual string DecryptToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var validationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return ((JwtSecurityToken)validatedToken).ToString();
        }
    }
}