using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string Anh { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
