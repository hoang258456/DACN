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
    public class AdShipperController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdShipperController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Ad/AdShipper
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shippers.ToListAsync());
        }

        // GET: Ad/AdShipper/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(m => m.HiperId == id);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // GET: Ad/AdShipper/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ad/AdShipper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HiperId,TenShiper,Sdt")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Ad/AdShipper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers.FindAsync(id);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        // POST: Ad/AdShipper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HiperId,TenShiper,Sdt")] Shipper shipper)
        {
            if (id != shipper.HiperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipperExists(shipper.HiperId))
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
            return View(shipper);
        }

        // GET: Ad/AdShipper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(m => m.HiperId == id);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // POST: Ad/AdShipper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipper = await _context.Shippers.FindAsync(id);
            _context.Shippers.Remove(shipper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipperExists(int id)
        {
            return _context.Shippers.Any(e => e.HiperId == id);
        }
    }
}
