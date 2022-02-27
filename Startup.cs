using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using LibApp.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LibApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDefaultIdentity<Customer>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Add(new ServiceDescriptor(typeof(IBookRepository), typeof(BookRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IGenreRepository), typeof(GenreRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IMembershipTypeRepository), typeof(MembershipTypeRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(ICustomerRepository), typeof(CustomerRepository), ServiceLifetime.Scoped));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}