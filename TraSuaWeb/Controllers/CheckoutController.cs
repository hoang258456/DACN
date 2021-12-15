using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Extension;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;

namespace TraSuaWeb.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DBtrasuaContext _context;
        public INotyfService _notifyService { get; }
        public CheckoutController(DBtrasuaContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public List<GiohangViewModel> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
                if (gh == default(List<GiohangViewModel>))
                {
                    gh = new List<GiohangViewModel>();
                }
                return gh;
            }
        }
        [Route("checkout.html", Name = "checkout")]
        public IActionResult Index(string returnUrl = null)
        {
            var cart = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                model.MaKh = khachhang.MaKh;
                model.TenKh = khachhang.TenKh;
                model.Email = khachhang.Email;
                model.Sðt = khachhang.Sðt;
                model.DiaChiGiaoHang = khachhang.DiaChi;

            }
            ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
            ViewBag.GioHang = cart;
            return View(model);
        }
        [HttpPost]
        [Route("checkout.html", Name = "checkout")]
        public IActionResult Index(MuaHangVM muahang)
        {
            var cart = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                model.MaKh = khachhang.MaKh;
                model.TenKh = khachhang.TenKh;
                model.Email = khachhang.Email;
                model.Sðt = khachhang.Sðt;
                model.DiaChiGiaoHang = khachhang.DiaChi;


                khachhang.LocationId = muahang.TInhThanh;
                khachhang.QuanHuyen = muahang.QuanHuyen;
                khachhang.PhuongXa = muahang.PhuongXa;
                khachhang.DiaChi = muahang.DiaChiGiaoHang;
                _context.Update(khachhang);
                _context.SaveChanges();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    HoaDon donhang = new HoaDon();
                    donhang.MaKh = model.MaKh;
                    donhang.DiaChiGiaoHang = model.DiaChiGiaoHang;
                    donhang.LocationId = model.QuanHuyen;
                    donhang.QuanHuyen = model.QuanHuyen;
                    donhang.PhuongXa = model.PhuongXa;

                    donhang.NgayDat = DateTime.Now;
                    donhang.TrangThai = true;
                    donhang.ThanhToan = false;

                    donhang.TongTien = Convert.ToInt32(cart.Sum(x => x.Tongtien));
                    _context.Add(donhang);
                    _context.SaveChanges();
                    foreach (var item in cart)
                    {
                        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                        chiTietHoaDon.MaHd = donhang.MaHd;
                        chiTietHoaDon.Soluong = donhang.Soluong;
                        chiTietHoaDon.Gia = item.sanpham.Gia;
                        chiTietHoaDon.MaSize = item.sanpham.MaSize;
                        chiTietHoaDon.MaSp = item.sanpham.MaSp;
                        _context.Add(chiTietHoaDon);
                    }
                    _context.SaveChanges();
                    HttpContext.Session.Remove("GioHang");
                    _notifyService.Success(" Đơn hàng thành công ");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
                ViewBag.GioHang = cart;
                return View(model);
            }
            ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
            ViewBag.GioHang = cart;
            return View(model);
        }
        //[Route("dat-hang-thanh-cong.html",Name ="Success")]
        //public IActionResult Success()
        //{
        //    try
        //    {
        //        var taikhoanID = HttpContext.Session.GetString("MaKh");
        //        if (string.IsNullOrEmpty(taikhoanID))
        //        {
        //            return RedirectToAction("Login", "Accounts", new { returnUrl = "/dat-hang-thanh-cong.html" });
        //        }
        //        var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
        //        var donhang = _context.HoaDons.Where(x => x.MaKh == Convert.ToInt32(taikhoanID)).OrderByDescending(x => x.ChiTietHoaDons);

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}