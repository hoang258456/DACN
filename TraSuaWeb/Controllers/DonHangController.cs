using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TraSuaWeb.ModelView;

namespace TraSuaWeb.Controllers
{
    public class DonHangController : Controller
    {
        private readonly DBtrasuaContext _context;
        public INotyfService _notifyService { get; }
        public DonHangController(DBtrasuaContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var taikhoanID = HttpContext.Session.GetString("MaKh");
                if (string.IsNullOrEmpty(taikhoanID))
                    return RedirectToAction("Login", "Accounts");
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                if (khachhang == null) return NotFound();
                var donhang = await _context.HoaDons
                    .Include(x => x.TrangThai)
                    .FirstOrDefaultAsync(m => m.MaHd == id && Convert.ToInt32(taikhoanID) == m.MaKh);
                if (donhang == null)
                {
                    return NotFound();
                }
                var chitietdonhang = _context.ChiTietHoaDons
                    .Include(x => x.MaSp)
                    .AsNoTracking()
                    .Where(x => x.MaHd == id)
                    .OrderBy(x => x.MaSize)
                    .ToList();
                XemDonHang donHang = new XemDonHang();
                donHang.DonHang = donhang;
                donHang.ChiTietDonHang = chitietdonhang;
                return PartialView("Details", donHang);
            }
            catch
            {
                return NotFound();
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
