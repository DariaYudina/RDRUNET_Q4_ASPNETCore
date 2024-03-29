using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Epam.ASPNETCore.TourOperator.BLL;
using Epam.ASPNETCore.TourOperator.DAL;
using Epam.ASPNETCore.TourOperator.IBLL;
using Epam.ASPNETCore.TourOperator.IDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Epam.ASPNETCore.TourOperator.WEBUI
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
            services.AddControllersWithViews();
            services.AddSingleton<ITourLogic, TourLogic>();
            services.AddSingleton<ITourDao, TourDao>(provider => new TourDao(Configuration.GetConnectionString("TourDB")));
            services.AddSingleton<ICountryLogic, CountryLogic>();
            services.AddSingleton<ICountryDao, CountryDao>(provider => new CountryDao(Configuration.GetConnectionString("TourDB")));
            services.AddSingleton<IRegionLogic, RegionLogic>();
            services.AddSingleton<IRegionDao, RegionDao>(provider => new RegionDao(Configuration.GetConnectionString("TourDB")));
            services.AddSingleton<ICityLogic, CityLogic>();
            services.AddSingleton<ICityDao, CityDao>(provider => new CityDao(Configuration.GetConnectionString("TourDB")));
            services.AddSingleton<IAreaLogic, AreaLogic>();
            services.AddSingleton<IAreaDao, AreaDao>(provider => new AreaDao(Configuration.GetConnectionString("TourDB")));
            services.AddAutoMapper(typeof(Startup));
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "RedisInstance";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Tour/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Tour}/{action=Index}/{id?}");
            });
        }


    }
}
