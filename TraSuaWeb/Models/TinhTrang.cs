using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class TinhTrang
    {
        public TinhTrang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int Id { get; set; }
        public string TrangThai { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
