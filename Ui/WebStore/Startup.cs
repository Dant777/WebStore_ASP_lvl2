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
using Microsoft.Extensions.Logging;
using WebStore.Clients.Services;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using WebStore.Infrastructure;
using WebStore.Interfaces;
using WebStore.Logger;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(SimpleActionFilter));
                options.Filters.Add(new SimpleActionFilter());
            });

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddTransient<IValueService, ValuesClient>();
            services.AddScoped<IProductService, ProductsClient>();
            services.AddScoped<IOrdersService, OrdersClient>();
            services.AddTransient<IUsersClient, UsersClient>();


            // Настройка Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                ;

            services.AddTransient<IUserStore<User>, CustomUserStore>();
            services.AddTransient<IUserRoleStore<User>, CustomUserStore>();
            services.AddTransient<IUserClaimStore<User>, CustomUserStore>();
            services.AddTransient<IUserPasswordStore<User>, CustomUserStore>();
            services.AddTransient<IUserTwoFactorStore<User>, CustomUserStore>();
            services.AddTransient<IUserEmailStore<User>, CustomUserStore>();
            services.AddTransient<IUserPhoneNumberStore<User>, CustomUserStore>();
            services.AddTransient<IUserLoginStore<User>, CustomUserStore>();
            services.AddTransient<IUserLockoutStore<User>, CustomUserStore>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            //Настрйоки для корзины товаров
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithRedirects("~/home/ErrorStatus/{0}");

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

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

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

            //var hello = Configuration["CustomHelloWorld"];

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(hello);
            //});
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