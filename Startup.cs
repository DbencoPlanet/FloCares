using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FloCares.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FloCares.Models;
using FloCares.Areas.Interface;
using FloCares.Areas.Services;
using FloCares.Models.Dtos;
using FloCares.Models.Entities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace FloCares
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            //services.AddSingleton<IAccountant, AccountantService>();
            //services.AddSingleton<IUpload, UploadService>();

            services.Configure<IdentityOptions>(options =>
            {
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz" +
                //"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                options.User.RequireUniqueEmail = true;

                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 4;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddControllersAsServices();


            services.AddScoped<IAccountant, AccountantService>();
            services.AddScoped<IUpload, UploadService>();
            services.AddScoped<IDoctor, DoctorService>();
            services.AddScoped<ISetting, SettingService>();
            services.AddScoped<IPatient, PatientService>();

            // Add application services.
            //services.AddTransient<IAccountant, AccountantService>();
            //services.AddTransient<IUpload, UploadService>();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
