using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserServer.Core.Configurations;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;
using JwtRegisteredClaimNamesNew = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UserServer.Core.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public TokenGenerator(UserManager<User> userManager, IOptions<JwtSettings> options)
        {
            _userManager = userManager;
            _jwtSettings = options.Value;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNamesNew.Sub, user.Id),
                new Claim(JwtRegisteredClaimNamesNew.Email, user.Email),
                new Claim(JwtRegisteredClaimNamesNew.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);
            claim.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds);

             return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
