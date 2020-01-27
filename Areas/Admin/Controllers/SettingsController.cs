using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloCares.Data;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Http;
using FloCares.Areas.Interface;
using Microsoft.AspNetCore.Hosting;
using FloCares.Areas.Services;

namespace FloCares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {

        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        private readonly ApplicationDbContext _context;
        private readonly ISetting _seting;

        public SettingsController (ApplicationDbContext context,
            ISetting seting,
            IUpload upload,
            IHostingEnvironment env)
          
        {

            _seting = seting;
            _upload = upload;
            _env = env;
            _context = context;
          

        }

        // GET: Admin/Settings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }

        // GET: Admin/Settings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Admin/Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting, List<IFormFile> logo, List<IFormFile> favicon)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(setting);
                //await _context.SaveChangesAsync();
                await _seting.Create(setting, logo, favicon);
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Admin/Settings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Setting setting, List<IFormFile> logo, List<IFormFile> favicon)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(setting);
                    //await _context.SaveChangesAsync();

                    await _seting.Edit(setting, logo, favicon);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
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
            return View(setting);
        }

        // GET: Admin/Settings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Admin/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}
