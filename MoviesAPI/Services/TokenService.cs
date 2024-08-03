
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesAPI.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim("email", user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gsfdgdgdsdfsdf5465"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddMinutes(10),
                    claims: claims,
                    signingCredentials: null
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
