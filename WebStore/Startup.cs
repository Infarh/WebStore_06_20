using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => 
                opt.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews(opt =>
            {
                //opt.Filters.Add<Filter>();
                //opt.Conventions.Add(); // Добавление/изменение соглашений MVC-приложения
            }).AddRazorRuntimeCompilation();

            services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, InMemoryProductData>();

            //services.AddTransient<TInterface, TService>();
            //services.AddScoped<TInterface, TService>();
            //services.AddSingleton<TInterface, TService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
           db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseWelcomePage("/welcome");

            //app.Use(async (context, next) =>
            //{
            //    //Действия над context до следующего элемента в конвейере
            //    await next(); // Вызов следующего промежуточного ПО в конвейере
            //    // Действия над context после следующего элемента в конвейере
            //});

            //app.UseMiddleware<TestMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(_Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
