using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class Tintuc
    {
        public int IdTt { get; set; }
        public string TieuDe { get; set; }
        public string AnhGt { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgaySua { get; set; }
        public bool AnHien { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
