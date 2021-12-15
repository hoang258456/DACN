using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraSuaWeb.ModelView
{
    public class dangkyViewModel
    {
        [Key]
        public int MaKh { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập Họ và tên")]
        public string TenKh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "ValidateEmail", controller: "Account")]
        public String Email { get; set; }
        [MaxLength(11)]
        [Required(ErrorMessage = "Vui lòng nhập Số Điện Thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Account")]
        public String Sðt { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public String DiaChi { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Bạn cần nhập mật khẩu tối thiểu 8 ký tự")]
        public String Password { get; set; }
        [MinLength(5, ErrorMessage = "Bạn cần nhập mật khẩu tối thiểu 8 ký tự")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Vui lòng nhập mật khẩu giống nhau")]
        public String ConfirmPassword { get; set; }
        public bool? Tinhtrang { get; set; }
    }
}