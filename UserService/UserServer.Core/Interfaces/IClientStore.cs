using Duende.IdentityServer.EntityFramework.Entities;


namespace UserServer.Core.Interfaces
{
    public interface IClientStore
    {
        Task<Client> FindClientByIdAsync(string clientId);
    }
}
