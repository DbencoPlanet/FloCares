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
    public class BedCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BedCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.BedCategory.ToListAsync());
        }

        // GET: Admin/BedCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedCategory = await _context.BedCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bedCategory == null)
            {
                return NotFound();
            }

            return View(bedCategory);
        }

        // GET: Admin/BedCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BedCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BedCategory bedCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bedCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bedCategory);
        }

        // GET: Admin/BedCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedCategory = await _context.BedCategory.FindAsync(id);
            if (bedCategory == null)
            {
                return NotFound();
            }
            return View(bedCategory);
        }

        // POST: Admin/BedCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BedCategory bedCategory)
        {
            if (id != bedCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bedCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BedCategoryExists(bedCategory.Id))
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
            return View(bedCategory);
        }

        // GET: Admin/BedCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bedCategory = await _context.BedCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bedCategory == null)
            {
                return NotFound();
            }

            return View(bedCategory);
        }

        // POST: Admin/BedCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bedCategory = await _context.BedCategory.FindAsync(id);
            _context.BedCategory.Remove(bedCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BedCategoryExists(int id)
        {
            return _context.BedCategory.Any(e => e.Id == id);
        }
    }
}
