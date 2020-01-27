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

namespace FloCares.Areas.Patients.Controllers
{
    [Area("Patients")]
    public class UserManagerController : Controller
    {
       
        private readonly IPatient _patient;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserManagerController(ApplicationDbContext context, 
            IPatient patient,IUpload upload, 
            IHostingEnvironment env, IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleMgr, 
            UserManager<ApplicationUser> userMrg)
        {
           
            _patient = patient;
            _upload = upload;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            roleManager = roleMgr;
            userManager = userMrg;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var acct = await _patient.ListAll();
            return View(acct);
        }

        // GET: Patient/UserManager
        public IActionResult NewPatient()
        {
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name");
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(5000000000000000000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewPatient(PatientDto model, List<IFormFile> files)
        {
          
            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _patient.Create(model);
                    if (succed == "true")
                    {
                        var user = await userManager.FindByNameAsync(model.EmailAddress);

                        var user1 = await _context.Patients.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        //try to update the Patient Reg
                        //Patient Reg
                        string numberid = user1.Id.ToString("D3");
                        user1.PatientReg = "PAT/" + numberid;

                        //try to update the user profile pict
                        //profile pic upload
                        var fileName = await _upload.UploadFile(files, _env);
                        user1.ProfilePicture = "/Uploads/" + fileName;
                        _context.Entry(user1).State = EntityState.Modified;
                        await _context.SaveChangesAsync();



                        TempData["success"] = "Patient with Username "+ model.Fullname + "Added Successfully";
                        return RedirectToAction("NewPatient");
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
            TempData["error"] = "Creation of new Patient not successful" + ee;
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name",model.BloodGroupId);
            return View(model);
        }


        public async Task<ActionResult> EditPatient(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pat = await _patient.Get(id);
            if (pat == null)
            {
                return NotFound();
            }
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name");
            return View(pat);
        }

        // POST: Patient/UserManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPatient(Patient model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                  
                    await _patient.Edit(model, files);

                    TempData["success"] = "Patient Info Edited Successfully";
                    return RedirectToAction("EditPatient");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            }
            TempData["error"] = "Unable to edit Patient Info";
            ViewData["BloodGroupId"] = new SelectList(_context.BloodGroup, "Id", "Name",model.BloodGroupId);
            return View(model);
        }

        // GET: Patient/UserManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acct = await _patient.Get(id);
            if (acct == null)
            {
                return NotFound();
            }

            return View(acct);
        }


        // GET: Patient/UserManager/Profile/5
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acct = await _patient.Get(id);
            if (acct == null)
            {
                return NotFound();
            }

            return View(acct);
        }

        // POST: Patient/UserManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pat = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(pat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

    }
}