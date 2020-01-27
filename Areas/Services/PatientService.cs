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
    public class PatientService : IPatient
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        public PatientService(ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg, IHttpContextAccessor httpContextAccessor, IUpload upload,
            IHostingEnvironment env)
        {
            _context = context;
            roleManager = roleMgr;
            userManager = userMrg;
            _httpContextAccessor = httpContextAccessor;
            _upload = upload;
            _env = env;
        }

        public async Task<string> Create(PatientDto model)
        {
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
                await userManager.AddToRoleAsync(user, "Patient");
                Patient patient = new Patient();
                patient.UserId = user.Id;
                patient.SurName = model.SurName;
                patient.FirstName = model.FirstName;
                patient.OtherName = model.OtherName;
                patient.EmailAddress = model.EmailAddress;
                patient.PhoneNo = model.PhoneNo;
                patient.MobileNo = model.MobileNo;
                patient.BloodGroupId = model.BloodGroupId;
                patient.Sex = model.Sex;
                patient.DOB = model.DOB;
                patient.ProfilePicture = model.ProfilePicture;
                patient.Address = model.Address;
                patient.Status = model.Status;
                patient.PatientReg = model.PatientReg;


                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();


                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(",", errors);
            return message;
        }

        public async Task Edit(Patient model, List<IFormFile> files)
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

        public async Task<List<Patient>> ListAll()
        {
            var pat = await _context.Patients.Include(x => x.User).Include(x => x.BloodGroup).OrderBy(x => x.SurName).ToListAsync();
            if (pat != null)
            {
                return pat;
            }
            return null;
        }

        public async Task Delete(int? id)
        {
            var item = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Patients.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Patient> Get(int? id)
        {
            var pat = await _context.Patients.Include(x => x.User).Include(x=>x.BloodGroup).FirstOrDefaultAsync(x => x.Id == id);
            if (pat != null)
            {
                return pat;
            }
            return null;
        }
    }
}
