using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ErrorMessage { get; set; }
        public string IpAddress { get; set; }
    }
}
