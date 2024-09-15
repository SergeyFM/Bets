using Microsoft.AspNetCore.Identity;

namespace UserServer.Core.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
