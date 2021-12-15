using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraSuaWeb.Models;
using webtrasua.helper;

namespace TraSuaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GioiThieuxController : Controller
    {
        private readonly DBtrasuaContext _context;

        public GioiThieuxController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/GioiThieux
        public async Task<IActionResult> Index()
        {
            return View(await _context.GioiThieus.ToListAsync());
        }

        // GET: Admin/GioiThieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus
                .FirstOrDefaultAsync(m => m.IdGt == id);
            if (gioiThieu == null)
            {
                return NotFound();
            }

            return View(gioiThieu);
        }

        // GET: Admin/GioiThieux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GioiThieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGt,TieuDe,Anh,MoTa")] GioiThieu gioiThieu, Microsoft.AspNetCore.Http.IFormFile fAnhGt)
        {
            if (ModelState.IsValid)
            {
                gioiThieu.TieuDe = Utilities.ToTitleCase(gioiThieu.TieuDe);
                if (fAnhGt != null)
                {
                    string extension = Path.GetExtension(fAnhGt.FileName);

                    string filename = $"{Utilities.SEOUrl(gioiThieu.TieuDe) }{extension}";
                    gioiThieu.Anh = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "AnhGioiThieu", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhGt.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(gioiThieu.Anh)) gioiThieu.Anh = "default.jpg";
                _context.Add(gioiThieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gioiThieu);
        }

        // GET: Admin/GioiThieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus.FindAsync(id);
            if (gioiThieu == null)
            {
                return NotFound();
            }
            return View(gioiThieu);
        }

        // POST: Admin/GioiThieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGt,TieuDe,Anh,MoTa")] GioiThieu gioiThieu)
        {
            if (id != gioiThieu.IdGt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioiThieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioiThieuExists(gioiThieu.IdGt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gioiThieu);
        }

        // GET: Admin/GioiThieux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieu = await _context.GioiThieus
                .FirstOrDefaultAsync(m => m.IdGt == id);
            if (gioiThieu == null)
            {
                return NotFound();
            }

            return View(gioiThieu);
        }

        // POST: Admin/GioiThieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioiThieu = await _context.GioiThieus.FindAsync(id);
            _context.GioiThieus.Remove(gioiThieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioiThieuExists(int id)
        {
            return _context.GioiThieus.Any(e => e.IdGt == id);
        }
    }
}
