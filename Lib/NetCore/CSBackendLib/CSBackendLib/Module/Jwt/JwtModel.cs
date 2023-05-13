using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BEBackendLib.Module.Jwt
{
    public class JwtModel
    {
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string IssuerSigningKey { get; set; } = string.Empty;
        public Claim[]? Claims { get; set; } = null;
        public int MinuteExpires { get; set; } = 5;
        public string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256;
    }
}
