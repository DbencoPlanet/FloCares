using FloCares.Models.Dtos;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Interface
{
    public interface IAccountant
    {
        Task <string> Create(AccountantDto model);
        Task Edit(Accountants model, List<IFormFile> files);
        Task<List<Accountants>> ListAll();
        Task Delete(int? id);
        Task<Accountants> Get(int? id);



    }
}
