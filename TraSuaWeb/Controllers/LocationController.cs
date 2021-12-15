using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.Controllers
{

    public class LocationController : Controller
    {
        private readonly DBtrasuaContext _context;

        public LocationController(DBtrasuaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult QuanHuyenList(int LocationId)
        {
            var QuanHuyens = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationId && x.Levels==2)
                .OrderBy(x => x.Name)
                .ToList();
            return Json(QuanHuyens);
        }
        public ActionResult PhuongXaList(int LocationID)
        {
            var Phuongxas = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationID && x.Levels==3)
                .OrderBy(x => x.Name)
                .ToList();
            return Json(Phuongxas);
        }


    }
}
