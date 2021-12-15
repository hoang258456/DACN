using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaSp { get; set; }
        public string MaSize { get; set; }
        public string MaLoai { get; set; }
        public string TenSp { get; set; }
        public string AnhSp { get; set; }
        public int? Gia { get; set; }
        public bool TinhTrang { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? GiaGiam { get; set; }
        public bool XacNhanGiam { get; set; }
        public DateTime? NgaySua { get; set; }
        public bool AnHien { get; set; }

        public virtual LoaiSp MaLoaiNavigation { get; set; }
        public virtual Size MaSizeNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
