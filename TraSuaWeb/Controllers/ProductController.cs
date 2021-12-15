using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly DBtrasuaContext _context;
        public ProductController(DBtrasuaContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = "Shop")]
        public IActionResult Index(int? page)
        {
            try {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lssanpham = _context.SanPhams.AsNoTracking()
                    .OrderByDescending(x => x.MaSp);
                PagedList<SanPham> models = new PagedList<SanPham>(lssanpham, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("index", "Home");
            }
        }
        [Route("tenloai.html", Name = "danhsachsanpham")]
        public IActionResult danhsachsanpham(string tenloai, int page = 1)
        {
            try
            {
                var pageSize = 9;
                var danhmuc = _context.LoaiSps.AsNoTracking().SingleOrDefault(x => x.TenLoai == tenloai);
                var lssanpham = _context.SanPhams.AsNoTracking()
                   .Where(x => x.MaLoai == danhmuc.MaLoai)
                    .OrderByDescending(x => x.MaSp);
                PagedList<SanPham> models = new PagedList<SanPham>(lssanpham, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.Currentloai = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("index", "Home");
            }
        }

        [Route("/{TenSp}-{MaSp}.html", Name = "chitietsanpham")]
        public IActionResult Details(int MaSp)
        {

            try
            {
                var sanpham = _context.SanPhams.Include(s => s.MaLoaiNavigation).Include(s => s.MaSizeNavigation).FirstOrDefault(x => x.MaSp == MaSp);
                if (sanpham == null)
                {
                    return RedirectToAction("index");
                    
                }
                var lssanpham = _context.SanPhams.AsNoTracking().Where(x => x.MaLoai == sanpham.MaLoai && x.MaSp != MaSp && x.TinhTrang == true)
                    .Take(4).ToList();
                ViewBag.Sanpham = lssanpham;
                return View(sanpham);
            }
            catch
            {
                return RedirectToAction("index", "Home");
            }      

        }
    }
}
