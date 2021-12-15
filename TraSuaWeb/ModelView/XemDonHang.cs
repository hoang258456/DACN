using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.ModelView
{
    public class XemDonHang
    {
        public HoaDon DonHang { get; set; }
        public List<ChiTietHoaDon> ChiTietDonHang { get; set; }
    }
}
