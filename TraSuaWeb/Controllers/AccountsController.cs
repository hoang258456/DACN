using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;
using webtrasua.helper;
using AspNetCoreHero.ToastNotification.Abstractions;
namespace TraSuaWeb.Controllers
{
    [Authorize]   
    public class AccountsController : Controller
    {

        private readonly DBtrasuaContext _context;
        public INotyfService _notifyService { get; }
        public AccountsController(DBtrasuaContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var IDtaikhoan = HttpContext.Session.GetString("MaKh");
            if (IDtaikhoan != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(IDtaikhoan));
                if (khachhang != null)
                {
                    var lsDonHang = _context.HoaDons
                        .Include(x => x.IdNavigation)
                        .AsNoTracking().Where(x => x.MaKh == khachhang.MaKh)
                        .OrderByDescending(x => x.NgayDat)
                        .ToList();
                    ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(String SDT)
        {
            try
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Sðt == SDT.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại:" + SDT + "Đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email: " + Email + " Đã đuoẹc sử dụng<br />");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "Dangky")]
        public IActionResult DangKyTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "Dangky")]
        public async Task<IActionResult> DangKyTaiKhoan(dangkyViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //String salt = Utilities.GetRandomKey();
                    KhachHang khachhang = new KhachHang
                    {
                        TenKh = taikhoan.TenKh,
                        Sðt = taikhoan.Sðt.Trim().ToLower(),
                        DiaChi = taikhoan.DiaChi,
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password.Trim()),
                        Tinhtrang = true

                    };
                    try
                    {
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();
                        HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                        var tailhoanID = HttpContext.Session.GetString("MaKh");
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.TenKh),
                            new Claim("MaKh",khachhang.MaKh.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    catch
                    {
                        return RedirectToAction("Dangkytaikhoan", "Accounts");
                    }

                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(String returnUrl = null)
        {
            var IDtaikhoan = HttpContext.Session.GetString("MaKh");
            if (IDtaikhoan != null)
            {

                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(dangnhapViewModel customer, String returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customer.Email);
                    if (!isEmail) return View(customer);
                    var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.Email);
                    if (khachhang == null) return RedirectToAction("DangKyTaiKhoan");

                    String pass = customer.Password.Trim();
                    if (khachhang.Password != pass)
                    {
                        _notifyService.Success("thong tin dang nhap chưa chinh xac");
                        return View(customer);
                    }
                    //kiem tra account co bi disblae hay ko
                    if (khachhang.Tinhtrang == false) return RedirectToAction("ThongBao", "Accounts");
                    HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                    var IDtaikhoan = HttpContext.Session.GetString("MaKh");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.TenKh),
                        new Claim("MaKh", khachhang.MaKh.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UsersAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    _notifyService.Success("Đăng nhập thành công");
                    return RedirectToAction("Dashboard", "Accounts");
                }
            }
            catch
            {
                return RedirectToAction("DangKyTaiKhoan", "Accounts");

            }
            return View(customer);
        }
        [HttpGet]
        [Route("dang-xuat.html", Name = "dangxuat")]
        public IActionResult dangxuat()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("MaKh");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult doimatkhau(doimatkhauViewModel model)
        {
            try
            {
                var IDtaikhoan = HttpContext.Session.GetString("MaKh");
                if (IDtaikhoan == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _context.KhachHangs.Find(Convert.ToInt32(IDtaikhoan));
                    if (taikhoan == null) return RedirectToAction("index", "Accounts");
                    var pass = model.PasswordNow.Trim();
                    if (pass == taikhoan.Password)
                    {
                        string passnew = model.Password.Trim();
                        taikhoan.Password = passnew;
                        _context.Update(taikhoan);
                        _context.SaveChanges();
                        _notifyService.Success("Thay đổi mật khẩu thành công");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                }

            }
            catch
            {
                _notifyService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Accounts");
            }
            _notifyService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}