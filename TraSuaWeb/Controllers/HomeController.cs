using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;

namespace TraSuaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBtrasuaContext _context;

        public HomeController(ILogger<HomeController> logger, DBtrasuaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM();
            var lssangpham = _context.SanPhams
                .Include(x => x.MaLoaiNavigation)
                .AsNoTracking()
            .Where(x => x.TinhTrang == true && x.AnHien==true)
            .OrderByDescending(x => x.NgayTao)
            .ToList();

            List<ProductHomeVM> lssanphamView = new List<ProductHomeVM>();

            var lsloai = _context.LoaiSps
                .AsNoTracking()
                .ToList();

            foreach (var item in lsloai)
            {

                ProductHomeVM producthome = new ProductHomeVM();
                producthome.loaisanpham = item;
                producthome.lssanpham = lssangpham.Where(x => x.MaLoai == item.MaLoai).ToList();
                producthome.loaisanpham.SanPhams = producthome.lssanpham;
                lssanphamView.Add(producthome);
            }

            var tintuc = _context.Tintucs.AsNoTracking().Take(3).ToList();
            model.sanphams = lssanphamView;
            model.tintucs = tintuc;
            ViewBag.AllProduct = lssangpham; 
            return View(model);
        }
        public IActionResult TimKiem()
        {
            return View();
        }
        //Tim Kiem Collection
        [HttpPost]
        public IActionResult TimKiem(IFormCollection collection, int? page)
        {
            int pageSize = 10;
            var pageNumber = (page ?? 1);
            string sTukhoa = collection["keyword"].ToString();
            List<SanPham> lstb = _context.SanPhams.Where(n => n.TenSp.Contains(sTukhoa)).ToList();
            if (lstb.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy kết quả!";
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstb.Count + " kết quả";
            return View(lstb.OrderBy(n => n.TenSp).ToPagedList(pageNumber, pageSize));
        }




        [Route("contact.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("about.html", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
