using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.Controllers
{
    public class Search : Controller
    {
        private readonly DBtrasuaContext _context;

        public Search(DBtrasuaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FindProduct(string keyword)
        {
            List<SanPham> ls = new List<SanPham>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductPatialView", null);
            }
            ls = _context.SanPhams
                .AsNoTracking()
                .Include(a => a.MaLoaiNavigation)
                .Include(a => a.MaSizeNavigation)
                .Where(x => x.TenSp.Contains(keyword))
                .OrderByDescending(x => x.TenSp)
                .Take(8)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListProductPatialView", null);
            }
            else
            {
                return PartialView("ListProductPatialView", ls);
            }
        }
    }
}
