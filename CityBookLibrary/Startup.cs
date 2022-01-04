using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Constants;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Contracts;
using Repository;
using Entities.DataOperator;
using Entities.DataOperator.Contracts;

namespace CityBookLibrary
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

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseInMemoryDatabase(ApplicationConstants.IN_MEMORY_IDENTITY_DB);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = ApplicationConstants.COOKIE_NAME;
                config.LoginPath = ApplicationConstants.APPLICATION_LOGIN_PATH;
                config.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
            });

          
            services.AddSingleton<IXMLPath, XMLPath>();
            services.AddSingleton<IXMLDeserializer, XMLFileDeserializer>();
            services.AddSingleton<IXMLSerializer, XMLSerializer>();
            services.AddSingleton<ICacheManager<RepositoryWrapper>, RepositoryCacheManager>();
            services.AddSingleton<IRepositoryWrapper, RepositoryWrapper>();           

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=Index}/{id?}");
            });
        }
    }
}
