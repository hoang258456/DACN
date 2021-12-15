using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraSuaWeb.ModelView
{
    public class dangnhapViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Dia chi Email")]
        public string Email { get; set; }

        [Display(Name = "Mặt khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu chỉ được sử dụng 8 ký tự")]
        public string Password { get; set; }
    }
    
}