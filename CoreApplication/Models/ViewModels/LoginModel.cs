using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
