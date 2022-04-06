using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using UDOT.Models;

namespace UDOT
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<IdentityOptions>(options =>
            {
                // Increased the Required Password Length to 10
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CrashDbConnection"]);
            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:IdentityConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddSingleton<InferenceSession>(
                        new InferenceSession("Models/best_reg_model.onnx")
                                                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("countypage",
                "{countySelect}/Page{pageNum}",
                new { Controller = "Home", action = "CrashDetailsList" });

                endpoints.MapControllerRoute(
                    "Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "CrashDetailsList", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "County", "{countySelect}", new { Controller = "Home", action = "CrashDetailsList" });
                
                //Need to do something with different endpoints or different something for different authorization levels

                endpoints.MapControllerRoute("countypage",
                "{countySelect}/Page{pageNum}",
                new { Controller = "Home", action = "AllList" });

                endpoints.MapControllerRoute(
                    "Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "AllList", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "County", "{countySelect}", new { Controller = "Home", action = "AllList" });

                

                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute("/admin/{*catchall}", "/Admin/Index");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
