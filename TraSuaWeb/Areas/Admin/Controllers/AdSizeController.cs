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
    public class AdSizeController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdSizeController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Ad/AdSize
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sizes.ToListAsync());
        }

        // GET: Ad/AdSize/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.MaSize == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Ad/AdSize/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ad/AdSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSize,LoaiSize,Gia")] Size size)
        {
            if (ModelState.IsValid)
            {
                _context.Add(size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: Ad/AdSize/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        // POST: Ad/AdSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSize,LoaiSize,Gia")] Size size)
        {
            if (id != size.MaSize)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(size.MaSize))
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
            return View(size);
        }

        // GET: Ad/AdSize/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.MaSize == id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Ad/AdSize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var size = await _context.Sizes.FindAsync(id);
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeExists(string id)
        {
            return _context.Sizes.Any(e => e.MaSize == id);
        }
    }
}
