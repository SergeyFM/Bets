using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.Core.Entities
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string Token {  get; set; }
        public DateTime Expiration {  get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
