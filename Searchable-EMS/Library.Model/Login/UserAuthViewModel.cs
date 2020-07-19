using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class UserAuthViewModel
    {
        public Guid UserId { get; set; }
        public string RealName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public List<Guid> RoleId { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string Token { get; set; }
    }
}
