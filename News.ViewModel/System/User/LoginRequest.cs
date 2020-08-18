using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.System.User
{
    public class LoginRequest
    {
        [Display(Name = "Tài khoảng")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Nhớ mật khẩu")]
        public bool RememberMe { get; set; }
    }
}
