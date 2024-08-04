
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesAPI.Services
{
    public class TokenService
    {
        private IConfiguration _builderConfiguration;

        public TokenService(IConfiguration builderConfiguration)
        {
            _builderConfiguration = builderConfiguration;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString()), // Ensure that Id is converted to string
                new Claim("email", user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString("yyyy-MM-dd")) // Use a specific date format
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builderConfiguration["SymmetricSecurityKey"]));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddMinutes(60),
                    claims: claims,
                    signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
