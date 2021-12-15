using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraSuaWeb.Models;

namespace TraSuaWeb.Areas.Ad.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class AdTuHangController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdTuHangController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Ad/AdTuHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.TuHangs.ToListAsync());
        }

        // GET: Ad/AdTuHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuHang = await _context.TuHangs
                .FirstOrDefaultAsync(m => m.MaTu == id);
            if (tuHang == null)
            {
                return NotFound();
            }

            return View(tuHang);
        }

        // GET: Ad/AdTuHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ad/AdTuHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTu,TenTu,TinhTrang")] TuHang tuHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tuHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tuHang);
        }

        // GET: Ad/AdTuHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuHang = await _context.TuHangs.FindAsync(id);
            if (tuHang == null)
            {
                return NotFound();
            }
            return View(tuHang);
        }

        // POST: Ad/AdTuHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTu,TenTu,TinhTrang")] TuHang tuHang)
        {
            if (id != tuHang.MaTu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tuHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TuHangExists(tuHang.MaTu))
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
            return View(tuHang);
        }

        // GET: Ad/AdTuHang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tuHang = await _context.TuHangs
                .FirstOrDefaultAsync(m => m.MaTu == id);
            if (tuHang == null)
            {
                return NotFound();
            }

            return View(tuHang);
        }

        // POST: Ad/AdTuHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tuHang = await _context.TuHangs.FindAsync(id);
            _context.TuHangs.Remove(tuHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TuHangExists(string id)
        {
            return _context.TuHangs.Any(e => e.MaTu == id);
        }
    }
}
