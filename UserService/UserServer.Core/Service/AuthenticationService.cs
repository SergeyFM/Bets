using Microsoft.AspNetCore.Identity;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Exceptions;
using UserServer.Core.Interfaces;

namespace UserServer.Core.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        //private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationService(UserManager<User> userManager, IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository, 
            ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            //_userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = await _tokenGenerator.GenerateToken(user); // Используем ITokenGenerator для генерации JWT-токена
                var refreshToken = new RefreshToken
                {
                    Token = await _tokenGenerator.GenerateToken(user),
                    UserId = user.Id,
                    Expiration = DateTime.UtcNow.AddDays(30)
                };
                await _refreshTokenRepository.AddAsync(refreshToken);
                return new AuthenticationResult { Token = token, RefreshToken = refreshToken.Token, Expiration = refreshToken.Expiration };
            }
            throw new InvalidCredentialsException();
        }

        public async Task LogoutAsync(string userId)
        {
            var refreshTokens = await _refreshTokenRepository.GetByUserIdAsync(userId);
            foreach (var token in refreshTokens)
            {
                await _refreshTokenRepository.RemoveAsync(token);
            }
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                var user = await _userManager.FindByIdAsync(token.UserId);
                var newToken = await _tokenGenerator.GenerateToken(user);
                return new AuthenticationResult { Token = newToken, RefreshToken = refreshToken, Expiration = token.Expiration };
            }
            throw new InvalidOperationException("Invalid refresh token");
        }
    }
}
