using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GTMS.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using GTMS.API.Helpers;
using AutoMapper;
using GTMS.API.Data.HocKyData;
using Swashbuckle.AspNetCore.Swagger;
using GTMS.API.Data.KhoaDaoTaoData;
using GTMS.API.Data.HuongNghienCuuData;
using GTMS.API.Data.KhoaData;
using GTMS.API.Data.LopData;
using GTMS.API.Data.GiangVienData;
using GTMS.API.Data.SinhVienData;
using Microsoft.AspNetCore.Identity;
using GTMS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using GTMS.API.Data.HuongDanDoAnData;
using GTMS.API.Data.PhanBienDoAnData;
using GTMS.API.Data.ThanhVienHoiDongBaoVeData;
using GTMS.API.Data.DoAnData;
using GTMS.API.Data.QuyDinhData;
using GTMS.API.Data.CaiDatData;

namespace GTMS.API
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
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "GTMS API", Description = "Graduation Thesis Management System API" });
            });
            services.AddCors();
            services.AddAutoMapper();
            services.AddScoped<IHuongNghienCuuRepository, HuongNghienCuuRepository>();
            services.AddScoped<IDoAnRepository, DoAnRepository>();
            services.AddScoped<IGiangVienRepository, GiangVienRepository>();
            services.AddScoped<IHocKyRepository, HocKyRepository>();
            services.AddScoped<IHuongDanDoAnRepository, HuongDanDoAnRepository>();
            services.AddScoped<IKhoaRepository, KhoaRepository>();
            services.AddScoped<IKhoaDaoTaoRepository, KhoaDaoTaoRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<IPhanBienDoAnRepository, PhanBienDoAnRepository>();
            services.AddScoped<IQuyDinhRepository, QuyDinhRepository>();
            services.AddScoped<ISinhVienRepository, SinhVienRepository>();
            services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
            services.AddScoped<ICaiDatRepository, CaiDatRepository>();
            //services.AddScoped<IThanhVienHDBVRepository, ThanhVienHDBVRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
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
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });
        }
    }
}
