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
using Microsoft.AspNetCore.Authorization;
namespace TraSuaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class TintucsController : Controller
    {
        private readonly DBtrasuaContext _context;

        public TintucsController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/Tintucs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tintucs.ToListAsync());
        }

        // GET: Admin/Tintucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .FirstOrDefaultAsync(m => m.IdTt == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // GET: Admin/Tintucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tintucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTt,TieuDe,AnhGt,MoTa,NgaySua,AnHien,NgayTao")] Tintuc tintuc, Microsoft.AspNetCore.Http.IFormFile fAnhTT)
        {
            if (ModelState.IsValid)
            {
                tintuc.TieuDe = Utilities.ToTitleCase(tintuc.TieuDe);
                if (fAnhTT != null)
                {
                    string extension = Path.GetExtension(fAnhTT.FileName);

                    string filename = $"{Utilities.SEOUrl(tintuc.TieuDe) }{extension}";
                    tintuc.AnhGt = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "AnhTinTuc", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhTT.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(tintuc.TieuDe)) tintuc.AnhGt = "default.jpg";
                tintuc.NgayTao = DateTime.Now;
                _context.Add(tintuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tintuc);
        }

        // GET: Admin/Tintucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs.FindAsync(id);
            if (tintuc == null)
            {
                return NotFound();
            }
            return View(tintuc);
        }

        // POST: Admin/Tintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTt,TieuDe,AnhGt,MoTa,NgaySua,AnHien,NgayTao")] Tintuc tintuc, Microsoft.AspNetCore.Http.IFormFile fAnhTT)
        {
            if (id != tintuc.IdTt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                tintuc.TieuDe = Utilities.ToTitleCase(tintuc.TieuDe);
                if (fAnhTT != null)
                {
                    string extension = Path.GetExtension(fAnhTT.FileName);

                    string filename = $"{Utilities.SEOUrl(tintuc.TieuDe) }{extension}";
                    tintuc.AnhGt = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "AnhTinTuc", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhTT.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(tintuc.TieuDe)) tintuc.AnhGt = "default.jpg";
                try
                {
                    tintuc.NgaySua = DateTime.Now;
                    _context.Update(tintuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucExists(tintuc.IdTt))
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
            return View(tintuc);
        }

        // GET: Admin/Tintucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .FirstOrDefaultAsync(m => m.IdTt == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // POST: Admin/Tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tintuc = await _context.Tintucs.FindAsync(id);
            _context.Tintucs.Remove(tintuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TintucExists(int id)
        {
            return _context.Tintucs.Any(e => e.IdTt == id);
        }
    }
}
