using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Clients.Services;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using WebStore.Infrastructure;
using WebStore.Interfaces;
using WebStore.Services;
using WebStore.Services.InMemory;
using WebStore.Services.Sql;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(SimpleActionFilter));
                options.Filters.Add(new SimpleActionFilter());
            });

            services.AddSingleton<IEmployeesData, InMemoryEmployeeData>();
            //services.AddScoped<IEmployeesData, InMemoryEmployeeData>();
            services.AddTransient<IValueService, ValuesClient>();
            services.AddScoped<IProductService, SqlProductService>();
            services.AddScoped<IOrdersService, SqlOrdersService>();

            services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();
                

          

            //Настрйоки для корзины товаров
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWelcomePage("/welcome");

            app.Map("/index", CustomIndexHandler);

            app.Use(async (context, next) =>
            {
                bool isError = false;
                if (isError)
                {
                    await context.Response.WriteAsync("Error occured. You're in custom pipeline module...");
                }
                else
                {
                    await next.Invoke();
                }
            });

            //app.UseMiddleware<TokenMiddleware>();

            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            var hello = Configuration["CustomHelloWorld"];

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(hello);
            });
        }

        private void CustomIndexHandler(IApplicationBuilder obj)
        {
            obj.Run(async context =>
            {
                await context.Response.WriteAsync("I'm your custom index handler");
            });
        }
    }
}
