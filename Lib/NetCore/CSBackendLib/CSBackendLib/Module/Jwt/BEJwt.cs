
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BEBackendLib.Module.Jwt
{
    public class BEJwt
    {
        private JwtModel? _jwtModel = null;

        public BEJwt(JwtModel jwtModel)
        {
            _jwtModel = jwtModel;
        }

        public string GenerateJwt()
        {
            string res = string.Empty;
            try
            {
                if (_jwtModel is null)
                    throw new Exception("JwtModel is null, please check again!");

                if (string.IsNullOrEmpty(_jwtModel.IssuerSigningKey))
                    throw new Exception("IssuerSigningKey is nullorEmpty, please check again!");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.IssuerSigningKey));
                var credentials = new SigningCredentials(securityKey, _jwtModel.Algorithm);

                var token = new JwtSecurityToken(_jwtModel.ValidIssuer,
                _jwtModel.ValidAudience,
                _jwtModel.Claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtModel.MinuteExpires),
                signingCredentials: credentials);

                res = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }
    }
}
