using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Areas.Interface
{
    public interface IUpload
    {
        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env);
    }
}
