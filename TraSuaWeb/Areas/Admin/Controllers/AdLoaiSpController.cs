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
    public class AdLoaispController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdLoaispController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/AdLoaisp
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiSps.ToListAsync());
        }

        // GET: Admin/AdLoaisp/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSp == null)
            {
                return NotFound();
            }

            return View(loaiSp);
        }

        // GET: Admin/AdLoaisp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdLoaisp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,Anh")] LoaiSp loaiSp, Microsoft.AspNetCore.Http.IFormFile fAnhL)
        {
            if (ModelState.IsValid)
            {

                loaiSp.TenLoai = Utilities.ToTitleCase(loaiSp.TenLoai);
                if (fAnhL != null)
                {
                    string extension = Path.GetExtension(fAnhL.FileName);

                    string filename = $"{Utilities.SEOUrl(loaiSp.TenLoai) }{extension}";
                    loaiSp.Anh = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Anhloaisp", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhL.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(loaiSp.TenLoai)) loaiSp.Anh = "default.jpg";
                _context.Add(loaiSp);
                _context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSp);
        }

        // GET: Admin/AdLoaisp/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps.FindAsync(id);
            if (loaiSp == null)
            {
                return NotFound();
            }
            return View(loaiSp);
        }

        // POST: Admin/AdLoaisp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLoai,TenLoai,Anh")] LoaiSp loaiSp, Microsoft.AspNetCore.Http.IFormFile fAnhL)
        {
            if (id != loaiSp.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                loaiSp.TenLoai = Utilities.ToTitleCase(loaiSp.TenLoai);
                if (fAnhL != null)
                {
                    string extension = Path.GetExtension(fAnhL.FileName);

                    string filename = $"{Utilities.SEOUrl(loaiSp.TenLoai) }{extension}";
                    loaiSp.TenLoai = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Anhloaisp", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhL.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(loaiSp.TenLoai)) loaiSp.Anh = "default.jpg";
                try
                {
                    _context.Update(loaiSp);
                    _context.SaveChanges();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSpExists(loaiSp.MaLoai))
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
            return View(loaiSp);
        }

        // GET: Admin/AdLoaisp/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSp == null)
            {
                return NotFound();
            }

            return View(loaiSp);
        }

        // POST: Admin/AdLoaisp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loaiSp = await _context.LoaiSps.FindAsync(id);
            _context.LoaiSps.Remove(loaiSp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSpExists(string id)
        {
            return _context.LoaiSps.Any(e => e.MaLoai == id);
        }
    }
}
