using FloCares.Models.Dtos;
using FloCares.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Interface
{
   public interface IPatient
    {
        Task<string> Create(PatientDto model);
        Task Edit(Patient model, List<IFormFile> files);
        Task<List<Patient>> ListAll();
        Task Delete(int? id);
        Task<Patient> Get(int? id);
    }
}
