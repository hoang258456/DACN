using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Extension;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;
namespace TraSuaWeb.Controllers
{
    public class GioHangController : Controller
    {
        private readonly DBtrasuaContext _context;
        public INotyfService _notifyService { get; }
        public GioHangController(DBtrasuaContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
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

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int SanphamID,String SizeSP, int? Soluong)
        {
            List<GiohangViewModel> giohang = GioHang;
            try
            {
                //thêm mới sản phẩm vào giỏ hàng
                GiohangViewModel item = giohang.SingleOrDefault(p => p.sanpham.MaSp == SanphamID);
                if (item != null)//đã có cập nhật số lượng
                {
                    item.Soluong = item.Soluong + Soluong.Value;
                    HttpContext.Session.Set<List<GiohangViewModel>>("GioHang",giohang);
                }
                else
                {
                    SanPham sp = _context.SanPhams.SingleOrDefault(p => p.MaSp == SanphamID);
                   
                    item = new GiohangViewModel
                    {
                        Soluong = Soluong.HasValue ? Soluong.Value : 1,
                        sanpham = sp
                       
                    };
                    giohang.Add(item);//thêm vào giỏ hàng
                }
                HttpContext.Session.Set<List<GiohangViewModel>>("GioHang", giohang);
                _notifyService.Success("thêm mới sản phẩm thành công");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
            // Thêm sản phẩm vào giỏ hàng

        }
        /* 
                    1. Thêm mới sản phẩm vào giỏ hàng
                    2. Cập nhật số lượng sản phẩm trong giỏ hàng
                    3. Xóa sản phẩm khỏi giỏ hàng
                    4. Xóa luôn giỏ hàng
         */
        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int SanphamID, int? Soluong)
        {
            var cart = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
            try
            {
                if (cart != null)
                {   
                    GiohangViewModel item = cart.SingleOrDefault(p => p.sanpham.MaSp == SanphamID);
                    if (item != null && Soluong.HasValue)
                    {
                        item.Soluong = Soluong.Value;
                    }
                    HttpContext.Session.Set<List<GiohangViewModel>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
            // Thêm sản phẩm vào giỏ hàng
        }
        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int SanphamID)
        {
            try
            {
                List<GiohangViewModel> giohang = GioHang;
                GiohangViewModel item = giohang.SingleOrDefault(p => p.sanpham.MaSp == SanphamID);
                if (item != null)
                {
                    giohang.Remove(item);
                }
                // Lưu lại Session
                HttpContext.Session.Set<List<GiohangViewModel>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }
        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            //var lsGioHang = GioHang;
            return View(GioHang); 
        }
    }
}