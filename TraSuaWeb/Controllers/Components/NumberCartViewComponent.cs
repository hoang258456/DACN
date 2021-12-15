using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Extension;
using TraSuaWeb.ModelView;

namespace TraSuaWeb.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var giohang = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
            return View(giohang);
        }
    }
}