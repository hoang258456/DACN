using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraSuaWeb.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Dia chi Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }
        [Display(Name = "Mặt khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(38, ErrorMessage = "Mật khẩu chỉ được sử dụng 30 ký tự")]
        public string Password { get; set; }

    }
}
