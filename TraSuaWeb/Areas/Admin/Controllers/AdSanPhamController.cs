using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using TraSuaWeb.Models;
using webtrasua.helper;

namespace TraSuaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdSanPhamController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdSanPhamController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/AdSanPham
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 9;
            var lssanpham = _context.SanPhams.AsNoTracking()
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaSizeNavigation)
                .OrderByDescending(x => x.MaSp);
            PagedList<SanPham> models = new PagedList<SanPham>(lssanpham, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            ViewData["MaLoai"] = new SelectList(_context.LoaiSps, "MaLoai", "TenLoai");
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "LoaiSize");
            return View(models);
        }

        // GET: Admin/AdSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaSizeNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/AdSanPham/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiSps, "MaLoai", "TenLoai");
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "LoaiSize");
            return View();
        }

        // POST: Admin/AdSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,MaSize,MaLoai,TenSp,AnhSp,Gia,TinhTrang,NgayTao,GiaGiam,XacNhanGiam,NgaySua,AnHien")] SanPham sanPham, Microsoft.AspNetCore.Http.IFormFile fAnhSp)
        {
            if (ModelState.IsValid)
            {
                sanPham.TenSp = Utilities.ToTitleCase(sanPham.TenSp);
                if (fAnhSp != null)
                {
                    string extension = Path.GetExtension(fAnhSp.FileName);

                    string filename = $"{Utilities.SEOUrl(sanPham.TenSp) }{extension}";
                    sanPham.AnhSp = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhSp.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(sanPham.AnhSp)) sanPham.AnhSp = "default.jpg";
                sanPham.NgayTao = DateTime.Now;
                _context.Add(sanPham);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSps, "MaLoai", "TenLoai");
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "LoaiSize");
            return View(sanPham);
        }

        // GET: Admin/AdSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSps, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "LoaiSize", sanPham.MaSize);
            return View(sanPham);
        }

        // POST: Admin/AdSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,MaSize,MaLoai,TenSp,AnhSp,Gia,TinhTrang,NgayTao,GiaGiam,XacNhanGiam,NgaySua,AnHien")] SanPham sanPham, Microsoft.AspNetCore.Http.IFormFile fAnhSp)
        {

            if (id != sanPham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                sanPham.TenSp = Utilities.ToTitleCase(sanPham.TenSp);
                if (fAnhSp != null)
                {
                    string extension = Path.GetExtension(fAnhSp.FileName);

                    string filename = $"{Utilities.SEOUrl(sanPham.TenSp) }{extension}";
                    sanPham.AnhSp = filename;
                    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await fAnhSp.CopyToAsync(stream);
                    }
                }
                if (string.IsNullOrEmpty(sanPham.AnhSp)) sanPham.AnhSp = "default.jpg";
                try
                {
                    sanPham.NgaySua = DateTime.Now;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSp))
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
            ViewData["MaLoai"] = new SelectList(_context.LoaiSps, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "LoaiSize", sanPham.MaSize);
            return View(sanPham);
        }

        // GET: Admin/AdSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaSizeNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/AdSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
    }
}
