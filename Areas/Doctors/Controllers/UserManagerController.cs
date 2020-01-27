using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloCares.Areas.Interface;
using FloCares.Data;
using FloCares.Models.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FloCares.Areas.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using FloCares.Models;
using FloCares.Models.Entities;

namespace FloCares.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class UserManagerController : Controller
    {

        private readonly IDoctor _doctor;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerController(ApplicationDbContext context,
            IDoctor doctor, IUpload upload,
            IHostingEnvironment env, IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userMrg)
        {

         
            _upload = upload;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            roleManager = roleMgr;
            userManager = userMrg;
            _context = context;
            _doctor = doctor;

        }

        public async Task<IActionResult> Index()
        {
            var doc = await _doctor.ListAll();
            return View(doc);
        }

        // GET: Doctor/UserManager
        public IActionResult NewDoctor()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DeptName");
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name");
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(5000000000000000000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewDoctor(DoctorDto model, List<IFormFile> files)
        {

            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _doctor.Create(model);
                    if (succed == "true")
                    {
                        var user = await userManager.FindByNameAsync(model.EmailAddress);

                        var user1 = await _context.Doctor.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        //try to update the Doctor Reg
                        //Doctor Reg
                        string numberid = user1.Id.ToString("D3");
                        user1.DoctorReg = "DOC/" + numberid;

                        //try to update the user profile pict
                        //profile pic upload
                        var fileName = await _upload.UploadFile(files, _env);
                        user1.ProfilePicture = "/Uploads/" + fileName;
                        _context.Entry(user1).State = EntityState.Modified;
                        await _context.SaveChangesAsync();



                        TempData["success"] = "Doctor with Username " + model.Fullname + "Added Successfully";
                        return RedirectToAction("NewDoctor");
                    }
                    else
                    {
                        TempData["error1"] = succed;
                    }
                }
                catch (Exception e)
                {
                    ee = e.ToString();
                }

            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["error"] = "Creation of new Doctor not successful" + ee;
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DeptName",model.DepartmentId);
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name",model.BloodGroupId);
            return View(model);
        }


        public async Task<ActionResult> EditDoctor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doc = await _doctor.Get(id);
            if (doc == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DeptName");
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name");
            return View(doc);
        }

        // POST: Doctor/UserManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDoctor(Doctor model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _doctor.Edit(model, files);

                    TempData["success"] = "Doctor Info Edited Successfully";
                    return RedirectToAction("EditDoctor");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            TempData["error"] = "Unable to edit Doctor Info";
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DeptName", model.DepartmentId);
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name", model.BloodGroupId);
            return View(model);
        }

        // GET: Doctor/UserManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _doctor.Get(id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }


        // GET: Doctor/UserManager/Profile/5
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _doctor.Get(id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Doctor/UserManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doc = await _context.Doctor.FindAsync(id);
            _context.Doctor.Remove(doc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }

    }
}