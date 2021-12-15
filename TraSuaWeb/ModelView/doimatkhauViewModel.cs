using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraSuaWeb.ModelView
{
    public class doimatkhauViewModel
    {
        [Key]
        public int MaKh { get; set; }
        [Display(Name = "Mật khẩu hiện tại")]
        public string PasswordNow { get; set; }
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Bạn cần nhập mật khẩu tối thiểu 8 ký tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Bạn cần nhập mật khẩu tối thiểu 8 ký tự")]
        [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
        public String ConfimPassword { get; set; }
    }
}