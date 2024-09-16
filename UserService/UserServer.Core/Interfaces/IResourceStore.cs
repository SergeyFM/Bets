using Duende.IdentityServer.Models;

namespace UserServer.Core.Interfaces
{
    public interface IResourceStore
    {
        Task<Resources> GetAllResourcesAsync();
    }
}
