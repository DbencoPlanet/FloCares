using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloCares.Data;
using FloCares.Models.Entities;

namespace FloCares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Beds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Beds.Include(b => b.BedCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Beds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bed = await _context.Beds
                .Include(b => b.BedCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bed == null)
            {
                return NotFound();
            }

            return View(bed);
        }

        // GET: Admin/Beds/Create
        public IActionResult Create()
        {
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName");
            return View();
        }

        // POST: Admin/Beds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bed bed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bed.BedCategoryId);
            return View(bed);
        }

        // GET: Admin/Beds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bed = await _context.Beds.FindAsync(id);
            if (bed == null)
            {
                return NotFound();
            }
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bed.BedCategoryId);
            return View(bed);
        }

        // POST: Admin/Beds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bed bed)
        {
            if (id != bed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BedExists(bed.Id))
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
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bed.BedCategoryId);
            return View(bed);
        }

        // GET: Admin/Beds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bed = await _context.Beds
                .Include(b => b.BedCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bed == null)
            {
                return NotFound();
            }

            return View(bed);
        }

        // POST: Admin/Beds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bed = await _context.Beds.FindAsync(id);
            _context.Beds.Remove(bed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BedExists(int id)
        {
            return _context.Beds.Any(e => e.Id == id);
        }
    }
}
