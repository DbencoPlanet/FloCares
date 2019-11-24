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
    public class BloodGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BloodGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.BloodGroup.ToListAsync());
        }

        // GET: Admin/BloodGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodGroup == null)
            {
                return NotFound();
            }

            return View(bloodGroup);
        }

        // GET: Admin/BloodGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BloodGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BloodGroup bloodGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodGroup);
        }

        // GET: Admin/BloodGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroup.FindAsync(id);
            if (bloodGroup == null)
            {
                return NotFound();
            }
            return View(bloodGroup);
        }

        // POST: Admin/BloodGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BloodGroup bloodGroup)
        {
            if (id != bloodGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodGroupExists(bloodGroup.Id))
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
            return View(bloodGroup);
        }

        // GET: Admin/BloodGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodGroup == null)
            {
                return NotFound();
            }

            return View(bloodGroup);
        }

        // POST: Admin/BloodGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodGroup = await _context.BloodGroup.FindAsync(id);
            _context.BloodGroup.Remove(bloodGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodGroupExists(int id)
        {
            return _context.BloodGroup.Any(e => e.Id == id);
        }
    }
}
