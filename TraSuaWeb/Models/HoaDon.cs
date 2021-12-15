using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaHd { get; set; }
        public int? MaKh { get; set; }
        public bool? ThanhToan { get; set; }
        public DateTime? NgayDat { get; set; }
        public string MaTu { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public int? TongTien { get; set; }
        public int? Soluong { get; set; }
        public int? HiperId { get; set; }
        public bool TrangThai { get; set; }
        public int? Id { get; set; }
        public int? LocationId { get; set; }
        public int? QuanHuyen { get; set; }
        public int? PhuongXa { get; set; }

        public virtual Shipper Hiper { get; set; }
        public virtual TinhTrang IdNavigation { get; set; }
        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual TuHang MaTuNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
