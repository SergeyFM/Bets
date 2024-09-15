using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServer.Core.DTO;

namespace UserServer.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(LoginDto loginDto);
        Task LogoutAsync(string userId);
        Task<AuthenticationResult> RefreshTokenAsync(string refreshToken);
    }
}
