using FloCares.Models.Dtos;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Interface
{
    public interface IDoctor
    {
        Task<string> Create(DoctorDto model);
        Task Edit(Doctor model, List<IFormFile> files);
        Task<List<Doctor>> ListAll();
        Task Delete(int? id);
        Task<Doctor> Get(int? id);
    }
}
