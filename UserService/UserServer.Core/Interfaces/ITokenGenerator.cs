using UserServer.Core.Entities;

namespace UserServer.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
