using System;
using System.Collections.Generic;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int HiperId { get; set; }
        public string TenShiper { get; set; }
        public string Sdt { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
