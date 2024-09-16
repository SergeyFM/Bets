using UserServer.Core.Entities;

namespace UserServer.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId);
    }
}
