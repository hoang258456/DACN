using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class Size
    {
        public Size()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            SanPhams = new HashSet<SanPham>();
        }

        public string MaSize { get; set; }
        public string LoaiSize { get; set; }
        public double? Gia { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
