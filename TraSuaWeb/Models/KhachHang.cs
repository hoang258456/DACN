using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string Sðt { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Tinhtrang { get; set; }
        public int? LocationId { get; set; }
        public int? QuanHuyen { get; set; }
        public int? PhuongXa { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
