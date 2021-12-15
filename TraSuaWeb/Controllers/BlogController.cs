using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.Controllers
{
   
    public class BlogController : Controller
    {
        private readonly DBtrasuaContext _context;
        public BlogController(DBtrasuaContext context)
        {
            _context = context;
        }
        [Route("blog.html",Name ="Blog")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 4;
            var lstintuc = _context.Tintucs.AsNoTracking()
                .OrderByDescending(x => x.IdTt);
            PagedList<Tintuc> models = new PagedList<Tintuc>(lstintuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        [Route("/tintuc/{TieuDe}-{IdTt}.html",Name = "tintuc")]
        public IActionResult Details(int IdTt)
        {
            var tintuc = _context.Tintucs.AsNoTracking().SingleOrDefault(x => x.IdTt == IdTt);
            if (tintuc == null)
            {
                return RedirectToAction("index");
            }
            return View(tintuc);
        }
    }
}




