using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuessApp.BLL.AuthRepoBLL;
using GuessApp.BLL.PostRepoBLL;
using GuessApp.BLL.ProfileRepoBLL;
using GuessApp.Core.Util;
using GuessApp.DAL.AuthRepoDAL;
using GuessApp.DAL.PostRepoDAL;
using GuessApp.DAL.ProfileRepoDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuessAppAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IAuthDAL, AuthDAL>();
            services.AddScoped<IAuthBLL, AuthBLL>();

            services.AddScoped<IPostDAL, PostDAL>();
            services.AddScoped<IPostBLL, PostBLL>();

            services.AddScoped<IProfileDal, ProfileDal>();
            services.AddScoped<IProfileBLL, ProfileBLL>();

            // register DbContext
            services.AddDbContext<DbGuessContext>();

            // use AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));


            // ------ Authorization ------

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true; // password will require digits

                options.Lockout.MaxFailedAccessAttempts = 5; // ardicil 5 defe sifre girmek mumkun olsun, sehv olarsa lock olsun
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 deqiqe sonra istifadeci lockdan cixsin

                options.Lockout.AllowedForNewUsers = true; // yeni userler ucun de Lock prinsipleri kecerli olsun
            });

            // Cookie configurations
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                //options.AccessDeniedPath = "/Auth/AccessDenied";
                options.SlidingExpiration = true; // cookie date-in yenilenmesi

                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".AspNetCore.Security.Cookie",
                    SameSite = SameSiteMode.Lax, // again CSRF attack, can use only domen and subdomens, if you wanna use this cookie only in domen then use SameSiteMode.Strict
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.Redirect("/api/Error/Unauthorized");
                }
                else if (response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    response.Redirect("/api/Error/NotFound");
                }
                else if (response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    response.Redirect("/api/Error/Forbidden");
                }
                else if (response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
                {
                    response.Redirect("/api/Error/MethodNotAllowed");
                }
            });

            app.UseStaticFiles();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
