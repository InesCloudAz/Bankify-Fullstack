using Bankify.Repository.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bankify.Services
{
    public class UserService
    {

        public string GenerateToken(User user)
        {
           var claims = new List<Claim>

            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Sid, user.UserID.ToString()), 
                new Claim(ClaimTypes.Role, user.Role),
            };


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("privatekey197354%¤%#098713)%¤?913864%%%%##"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5146",
                    audience: "http://localhost:5146",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials);


            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
