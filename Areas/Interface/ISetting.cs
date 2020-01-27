using FloCares.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Interface
{
    public interface ISetting
    {
        Task Create(Setting model, List<IFormFile> logo, List<IFormFile> favicon);
        Task Edit(Setting model, List<IFormFile> logo, List<IFormFile> favicon);
        Task<List<Setting>> ListAll();
        Task Delete(int? id);
        Task<Setting> Get(int? id);
    }
}
