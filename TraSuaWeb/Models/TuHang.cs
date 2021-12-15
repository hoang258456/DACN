using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class TuHang
    {
        public TuHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string MaTu { get; set; }
        public string TenTu { get; set; }
        public bool TinhTrang { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
