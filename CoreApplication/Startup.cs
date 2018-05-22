using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace CoreApplication
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
            services.AddDbContext<BookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<UserIdentity, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 3;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            }
                )
                .AddEntityFrameworkStores<ApplicationContext>();       

            services.AddTransient<IAccountRepository, EFAccountRepository>();
            services.AddTransient<IBookRepository, EFBookRepository>();
            services.AddTransient<IGenreRepository, EFBookRepository>();
            services.AddTransient<IAuthorRepository, EFBookRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
            services.AddMvc();
            services.AddSession();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        
            app.UseBrowserLink();
            app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");           
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "Books/GenreId{genre:int}/Page{Page:int}",
                    defaults: new { controller = "Book", action = "BooksList" });               
                routes.MapRoute(
                    name: null,
                    template: "Books/Page{Page:int}",
                    defaults: new { controller = "Book", action = "BooksList" });
                routes.MapRoute(
                    name: null,
                    template: "Books",
                    defaults: new { controller = "Book", action = "BooksList" , Page = 1});
                routes.MapRoute(
                    name: null,
                    template: "EditPage/Page{p:int}",
                    defaults: new { controller = "Book", action = "BooksEditPage", p = 1 });
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Account", action = "Index"});
                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");
            });

           


        }
    }
}
