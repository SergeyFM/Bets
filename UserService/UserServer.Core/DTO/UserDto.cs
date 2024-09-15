using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.Core.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string? _userName;
        public string UserName {
            get
            {
                if (_userName == null)
                {
                    return Email;
                }

                return _userName;
            } 
            set
            {
                _userName = value;
            } 
        }
    }
}
