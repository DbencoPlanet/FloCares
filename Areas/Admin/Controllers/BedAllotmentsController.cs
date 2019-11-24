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
    public class BedAllotmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedAllotmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BedAllotments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BedAllotments.Include(b => b.BedCategory).Include(b => b.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/BedAllotments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedAllotment = await _context.BedAllotments
                .Include(b => b.BedCategory)
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bedAllotment == null)
            {
                return NotFound();
            }

            return View(bedAllotment);
        }

        // GET: Admin/BedAllotments/Create
        public IActionResult Create()
        {
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Fullname");
            return View();
        }

        // POST: Admin/BedAllotments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BedAllotment bedAllotment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bedAllotment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bedAllotment.BedCategoryId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Fullname", bedAllotment.PatientId);
            return View(bedAllotment);
        }

        // GET: Admin/BedAllotments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedAllotment = await _context.BedAllotments.FindAsync(id);
            if (bedAllotment == null)
            {
                return NotFound();
            }
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bedAllotment.BedCategoryId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Fullname", bedAllotment.PatientId);
            return View(bedAllotment);
        }

        // POST: Admin/BedAllotments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BedAllotment bedAllotment)
        {
            if (id != bedAllotment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bedAllotment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BedAllotmentExists(bedAllotment.Id))
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
            ViewData["BedCategoryId"] = new SelectList(_context.BedCategory, "Id", "BedName", bedAllotment.BedCategoryId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Fullname", bedAllotment.PatientId);
            return View(bedAllotment);
        }

        // GET: Admin/BedAllotments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedAllotment = await _context.BedAllotments
                .Include(b => b.BedCategory)
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bedAllotment == null)
            {
                return NotFound();
            }

            return View(bedAllotment);
        }

        // POST: Admin/BedAllotments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bedAllotment = await _context.BedAllotments.FindAsync(id);
            _context.BedAllotments.Remove(bedAllotment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BedAllotmentExists(int id)
        {
            return _context.BedAllotments.Any(e => e.Id == id);
        }
    }
}
