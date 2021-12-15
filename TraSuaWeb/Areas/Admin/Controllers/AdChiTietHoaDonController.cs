using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraSuaWeb.Models;

namespace TraSuaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class AdChiTietHoaDonController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdChiTietHoaDonController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Ad/AdChiTietHoaDon
        public async Task<IActionResult> Index()
        {
            var dBtrasuaContext = _context.ChiTietHoaDons.Include(c => c.MaHdNavigation).Include(c => c.MaSizeNavigation).Include(c => c.MaSpNavigation);
            return View(await dBtrasuaContext.ToListAsync());
        }

        // GET: Ad/AdChiTietHoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons
                .Include(c => c.MaHdNavigation)
                .Include(c => c.MaSizeNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // GET: Ad/AdChiTietHoaDon/Create
        public IActionResult Create()
        {
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaTu");
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "MaSize");
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaLoai");
            return View();
        }

        // POST: Ad/AdChiTietHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,MaHd,Soluong,Gia,MaSize")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaTu", chiTietHoaDon.MaHd);
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "MaSize", chiTietHoaDon.MaSize);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaLoai", chiTietHoaDon.MaSp);
            return View(chiTietHoaDon);
        }

        // GET: Ad/AdChiTietHoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons.FindAsync(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaTu", chiTietHoaDon.MaHd);
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "MaSize", chiTietHoaDon.MaSize);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaLoai", chiTietHoaDon.MaSp);
            return View(chiTietHoaDon);
        }

        // POST: Ad/AdChiTietHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,MaHd,Soluong,Gia,MaSize")] ChiTietHoaDon chiTietHoaDon)
        {
            if (id != chiTietHoaDon.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHoaDonExists(chiTietHoaDon.MaSp))
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
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaTu", chiTietHoaDon.MaHd);
            ViewData["MaSize"] = new SelectList(_context.Sizes, "MaSize", "MaSize", chiTietHoaDon.MaSize);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaLoai", chiTietHoaDon.MaSp);
            return View(chiTietHoaDon);
        }

        // GET: Ad/AdChiTietHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons
                .Include(c => c.MaHdNavigation)
                .Include(c => c.MaSizeNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // POST: Ad/AdChiTietHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHoaDon = await _context.ChiTietHoaDons.FindAsync(id);
            _context.ChiTietHoaDons.Remove(chiTietHoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHoaDonExists(int id)
        {
            return _context.ChiTietHoaDons.Any(e => e.MaSp == id);
        }
    }
}
