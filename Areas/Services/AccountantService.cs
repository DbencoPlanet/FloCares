using FloCares.Areas.Interface;
using FloCares.Data;
using FloCares.Models;
using FloCares.Models.Dtos;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FloCares.Areas.Services
{
    public class AccountantService : IAccountant
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        public AccountantService(ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg, IHttpContextAccessor httpContextAccessor,IUpload upload,
            IHostingEnvironment env)
        {
            _context = context;
            roleManager = roleMgr;
            userManager = userMrg;
            _httpContextAccessor = httpContextAccessor;
            _upload = upload;
            _env = env;
        }

        public async Task<string> Create(AccountantDto model)
        {
            //var officer = _httpContextAccessor.HttpContext.User.Identity.Name;

            //if (officer == "SuperAdmin")
            //{
            //    officer = "Admin";
            //}

            var user = new ApplicationUser
            {
                UserName = model.Email,
                SurName = model.SurName,
                Email = model.Email,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Sex = model.Sex,
                Address = model.Address,
                EntityStatus = model.Status

            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Accountant");
                Accountants accountant = new Accountants();
                accountant.UserId = user.Id;
                accountant.SurName = model.SurName;
                accountant.FirstName = model.SurName;
                accountant.OtherName = model.OtherName;
                accountant.EmailAddress = model.Email;
                accountant.PhoneNo = model.PhoneNo;
                accountant.MobileNo = model.MobileNo;
                accountant.Sex = model.Sex;
                accountant.Address = model.Address;
                accountant.Status = model.Status;
                accountant.ProfilePicture = model.ProfilePicture;

                _context.Accountants.Add(accountant);
                await _context.SaveChangesAsync();


                //var acctReg = await _context.Accountants.FirstOrDefaultAsync(x => x.UserId == user.Id);
                //string numberid = acctReg.Id.ToString("D3");
                //acctReg.AccountantReg = "ACCT/" + numberid;
                //_context.Entry(acctReg).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(",", errors);
            return message;
        }

        public async Task Edit(Accountants model, List<IFormFile> files)
        {
            var fileName = await _upload.UploadFile(files, _env);
            model.ProfilePicture = "/Uploads/" + fileName;
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Accountants>> ListAll()
        {
            var acct = await _context.Accountants.Include(x => x.User).OrderBy(x => x.SurName).ToListAsync();
            if (acct != null)
            {
                return acct;
            }
            return null;
        }

        public async Task Delete(int? id)
        {
            var item = await _context.Accountants.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Accountants.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Accountants> Get(int? id)
        {
            var acct = await _context.Accountants.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == id);
            if(acct != null)
            {
                return acct;
            }
            return null;
        }
    }
}
