using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using TraSuaWeb.Models;

namespace TraSuaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdHoaDonController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AdHoaDonController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/AdHoaDon
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lshoadon = _context.HoaDons.AsNoTracking()
                .Include(s => s.Hiper)
                .Include(s => s.IdNavigation)
                .Include(s => s.MaKhNavigation)
                .Include(s => s.MaTuNavigation)
                .OrderByDescending(x => x.MaHd);
            PagedList<HoaDon> models = new PagedList<HoaDon>(lshoadon, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["HiperId"] = new SelectList(_context.Shippers, "HiperId", "HiperId");
            ViewData["Id"] = new SelectList(_context.TinhTrangs, "TrangThai", "TrangThai");
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaTu"] = new SelectList(_context.TuHangs, "MaTu", "MaTu");
            return View(models);

            //var dBtrasuaContext = _context.HoaDons.Include(h => h.Hiper).Include(h => h.IdNavigation).Include(h => h.MaKhNavigation).Include(h => h.MaTuNavigation);
            //return View(await dBtrasuaContext.ToListAsync());
        }

        // GET: Admin/AdHoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.Hiper)
                .Include(h => h.IdNavigation)
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaTuNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: Admin/AdHoaDon/Create
        public IActionResult Create()
        {
            ViewData["HiperId"] = new SelectList(_context.Shippers, "HiperId", "HiperId");
            ViewData["Id"] = new SelectList(_context.TinhTrangs, "TrangThai", "TrangThai");
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaTu"] = new SelectList(_context.TuHangs, "MaTu", "MaTu");
            return View();
        }

        // POST: Admin/AdHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHd,MaKh,ThanhToan,NgayDat,MaTu,DiaChiGiaoHang,TongTien,Soluong,HiperId,TrangThai,Id")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HiperId"] = new SelectList(_context.Shippers, "HiperId", "HiperId", hoaDon.HiperId);
            ViewData["Id"] = new SelectList(_context.TinhTrangs, "TrangThai", "TrangThai", hoaDon.Id);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaTu"] = new SelectList(_context.TuHangs, "MaTu", "MaTu", hoaDon.MaTu);
            return View(hoaDon);
        }

        // GET: Admin/AdHoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["HiperId"] = new SelectList(_context.Shippers, "HiperId", "HiperId", hoaDon.HiperId);
            ViewData["Id"] = new SelectList(_context.TinhTrangs, "TrangThai", "TrangThai", hoaDon.Id);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaTu"] = new SelectList(_context.TuHangs, "MaTu", "MaTu", hoaDon.MaTu);
            return View(hoaDon);
        }

        // POST: Admin/AdHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHd,MaKh,ThanhToan,NgayDat,MaTu,DiaChiGiaoHang,TongTien,Soluong,HiperId,TrangThai,Id")] HoaDon hoaDon)
        {
            if (id != hoaDon.MaHd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.MaHd))
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
            ViewData["HiperId"] = new SelectList(_context.Shippers, "HiperId", "HiperId", hoaDon.HiperId);
            ViewData["Id"] = new SelectList(_context.TinhTrangs, "TrangThai", "TrangThai", hoaDon.Id);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaTu"] = new SelectList(_context.TuHangs, "MaTu", "MaTu", hoaDon.MaTu);
            return View(hoaDon);
        }

        // GET: Admin/AdHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.Hiper)
                .Include(h => h.IdNavigation)
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaTuNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/AdHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }
    }
}
