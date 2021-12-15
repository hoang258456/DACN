using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.ModelView
{
    public class GiohangViewModel
    {
        public SanPham sanpham { get; set; }
        public Size size { get; set; }
        public HoaDon hoadon { get; set; }
        public int Soluong { get; set; }
        public double Tongtien => Soluong * /*(*/sanpham.Gia.Value /*+ size.Gia.Value)*/;
    }
}
