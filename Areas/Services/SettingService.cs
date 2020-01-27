using FloCares.Areas.Interface;
using FloCares.Data;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Services
{
    public class SettingService : ISetting
    {
        private readonly ApplicationDbContext _context;
        private readonly IUpload _upload;
        private readonly IHostingEnvironment _env;
        public SettingService(ApplicationDbContext context,
             IUpload upload,
            IHostingEnvironment env)
        {
            _context = context;
            _upload = upload;
            _env = env;
        }
        public async Task Create(Setting model, List<IFormFile> logo, List<IFormFile> favicon)
        {
            if (logo.Count == 0)
            {
                model.Logo = model.Logo;

            }
            else
            {
                var fileName = await _upload.UploadFile(logo, _env);
                model.Logo = "/Uploads/" + fileName;
            }

            if (favicon.Count == 0)
            {
                model.Favicon = model.Favicon;

            }
            else
            {
                var fileName = await _upload.UploadFile(favicon, _env);
                model.Favicon = "/Uploads/" + fileName;
            }
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Settings.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(Setting model, List<IFormFile> logo, List<IFormFile> favicon)
        {
            if (logo.Count == 0)
            {
                model.Logo = model.Logo;

            }
            else
            {
                var fileName = await _upload.UploadFile(logo, _env);
                model.Logo = "/Uploads/" + fileName;
            }

            if (favicon.Count == 0)
            {
                model.Favicon = model.Favicon;

            }
            else
            {
                var fileName = await _upload.UploadFile(favicon, _env);
                model.Favicon = "/Uploads/" + fileName;
            }
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Setting> Get(int? id)
        {
            var sett = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (sett != null)
            {
                return sett;
            }
            return null;
        }

        public async Task<List<Setting>> ListAll()
        {
            var sett = await _context.Settings.OrderBy(x => x.ApplicationTitle).ToListAsync();
            if (sett != null)
            {
                return sett;
            }
            return null;
        }
    }
}
