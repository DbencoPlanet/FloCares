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
    public class DoctorService : IDoctor
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        public DoctorService(ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg, IHttpContextAccessor httpContextAccessor, IUpload upload,
            IHostingEnvironment env)
        {
            _context = context;
            roleManager = roleMgr;
            userManager = userMrg;
            _httpContextAccessor = httpContextAccessor;
            _upload = upload;
            _env = env;
        }

        public async Task<string> Create(DoctorDto model)
        {
            //var officer = _httpContextAccessor.HttpContext.User.Identity.Name;

            //if (officer == "SuperAdmin")
            //{
            //    officer = "Admin";
            //}

            var user = new ApplicationUser
            {
                UserName = model.EmailAddress,
                SurName = model.SurName,
                Email = model.EmailAddress,
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
                await userManager.AddToRoleAsync(user, "Doctor");
                Doctor doctor = new Doctor();
                doctor.UserId = user.Id;
                doctor.SurName = model.SurName;
                doctor.FirstName = model.SurName;
                doctor.OtherName = model.OtherName;
                doctor.EmailAddress = model.EmailAddress;
                doctor.PhoneNo = model.PhoneNo;
                doctor.MobileNo = model.MobileNo;
                doctor.Sex = model.Sex;
                doctor.Address = model.Address;
                doctor.Status = model.Status;
                doctor.ProfilePicture = model.ProfilePicture;
                doctor.Address = model.Address;
                doctor.Biography = model.Biography;
                doctor.BloodGroupId = model.BloodGroupId;
                doctor.DepartmentId = model.DepartmentId;
                doctor.Designation = model.Designation;
                doctor.Specialist = model.Specialist;
                doctor.Education = model.Education;

                _context.Doctor.Add(doctor);
                await _context.SaveChangesAsync();


                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(",", errors);
            return message;
        }

        public async Task Edit(Doctor model, List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                model.ProfilePicture = model.ProfilePicture;

            }
            else
            {
                var fileName = await _upload.UploadFile(files, _env);
                model.ProfilePicture = "/Uploads/" + fileName;
            }
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> ListAll()
        {
            var doc = await _context.Doctor.Include(x => x.User).Include(x=>x.Department).Include(x=>x.BloodGroup).OrderBy(x => x.SurName).ToListAsync();
            if (doc != null)
            {
                return doc;
            }
            return null;
        }

        public async Task Delete(int? id)
        {
            var item = await _context.Doctor.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Doctor.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Doctor> Get(int? id)
        {
            var doc = await _context.Doctor.Include(x => x.User).Include(x => x.Department).Include(x => x.BloodGroup).FirstOrDefaultAsync(x => x.Id == id);
            if (doc != null)
            {
                return doc;
            }
            return null;
        }
    }
}
