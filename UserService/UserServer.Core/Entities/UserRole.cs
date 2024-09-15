using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.Core.Entities
{
    public class UserRole
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
