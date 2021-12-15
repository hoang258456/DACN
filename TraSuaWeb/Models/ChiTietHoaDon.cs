using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class ChiTietHoaDon
    {
        public int MaSp { get; set; }
        public int MaHd { get; set; }
        public int? Soluong { get; set; }
        public double? Gia { get; set; }
        public string MaSize { get; set; }
        public int MaCthd { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual Size MaSizeNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
