using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraSuaWeb.ModelView
{
    public class MuaHangVM
    {
        [Key]
        public int MaKh { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập họ và tên")]
        public string TenKh { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public String Sðt { get; set; }
        [Required(ErrorMessage = "Địa chỉ nhận hàng")]
        public string DiaChiGiaoHang { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn tỉnh thành")]
        public int TInhThanh { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn quận huyện")]
        public int QuanHuyen { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phường xã")]
        public int PhuongXa { get; set; }
        public int ThanhToan { get; set; }




    }
}
