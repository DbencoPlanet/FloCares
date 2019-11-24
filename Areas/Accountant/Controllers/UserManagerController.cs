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

namespace FloCares.Areas.Accountant.Controllers
{
    [Area("Accountant")]
    public class UserManagerController : Controller
    {
       
        private readonly IAccountant _accountant;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserManagerController(ApplicationDbContext context, 
            IAccountant accountant,IUpload upload, 
            IHostingEnvironment env, IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleMgr, 
            UserManager<ApplicationUser> userMrg)
        {
           
            _accountant = accountant;
            _upload = upload;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            roleManager = roleMgr;
            userManager = userMrg;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var acct = await _accountant.ListAll();
            return View(acct);
        }

        // GET: Accountant/UserManager
        public IActionResult NewAccountant()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(5000000000000000000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAccountant(AccountantDto model, List<IFormFile> files)
        {
          
            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _accountant.Create(model);
                    if (succed == "true")
                    {
                        var user = await userManager.FindByNameAsync(model.Email);

                        var user1 = await _context.Accountants.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        //try to update the Accountant Reg
                        //Accountant Reg
                        string numberid = user1.Id.ToString("D3");
                        user1.AccountantReg = "ACCT/" + numberid;

                        //try to update the user profile pict
                        //profile pic upload
                        var fileName = await _upload.UploadFile(files, _env);
                        user1.ProfilePicture = "/Uploads/" + fileName;
                        _context.Entry(user1).State = EntityState.Modified;
                        await _context.SaveChangesAsync();



                        TempData["success"] = "Accountant with Username "+ model.Fullname + "Added Successfully";
                        return RedirectToAction("NewAccountant");
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
            TempData["error"] = "Creation of new Accountant not successful" + ee;
            return View(model);
        }


        public async Task<ActionResult> EditAccountant(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var acct = await _accountant.Get(id);
            if (acct == null)
            {
                return NotFound();
            }
            return View(acct);
        }

        // POST: Accountant/UserManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAccountant(Accountants model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                  
                    await _accountant.Edit(model, files);

                    TempData["success"] = "Accountant Info Edited Successfully";
                    return RedirectToAction("EditAccountant");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountantExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            }
            TempData["error"] = "Unable to edit Accountant Info";
            return View(model);
        }

        // GET: Accountant/UserManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acct = await _accountant.Get(id);
            if (acct == null)
            {
                return NotFound();
            }

            return View(acct);
        }


        // GET: Accountant/UserManager/Profile/5
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acct = await _accountant.Get(id);
            if (acct == null)
            {
                return NotFound();
            }

            return View(acct);
        }

        // POST: Accountant/UserManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acct = await _context.Accountants.FindAsync(id);
            _context.Accountants.Remove(acct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountantExists(int id)
        {
            return _context.Accountants.Any(e => e.Id == id);
        }

    }
}