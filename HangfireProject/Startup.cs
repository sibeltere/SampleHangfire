using System;
using Hangfire;
using Hangfire.SqlServer;
using HangfireProject.BackgroundJob.Filters;
using HangfireProject.BackgroundJob.Schedules;
using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.BusinessLayer.Concrete;
using HangfireProject.Configurations;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.DataLayer.Concrete;
using HangfireProject.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HangfireProject
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
             AddCookie(option =>
             {
                 option.LoginPath = new PathString("/Login/Login");
                 option.ExpireTimeSpan = TimeSpan.FromDays(1);
             });


            services.AddControllersWithViews();
            services.AddControllers(options => options.EnableEndpointRouting = false);


            var connectionString = Configuration["ConnectionStrings:ProjectDev"];
            services.AddDbContext<HangfireDbContext>(option => option.UseSqlServer(connectionString));

            var hangfireConnectionString = Configuration["ConnectionStrings:HangfireDev"];
            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                config.UseSqlServerStorage(hangfireConnectionString, option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));

            });

            //dependecy

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserCrendentialsService, UserCrendentialsService>();
            services.AddScoped<IUserDal, UserDAL>();
            services.AddScoped<IUserCredentialsDAL, UserCrendentialsDAL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseHangfireDashboard("/myhangfire", new DashboardOptions
            {
                DashboardTitle = "Sibel Tere Hangfire DashBoard",  // Dashboard sayfasýna ait Baþlýk alanýný deðiþtiririz.
                //AppPath = "/Home/HangfireAbout",                     // Dashboard üzerinden "back to site" button
                Authorization = new[] { new HangfireDashboardAuthorizationFilter()},   // Güvenlik için Authorization Ýþlemleri
                AppPath = "/Login/Logout"
            });

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                /*  Hangfire Server, planlanan iþleri sýralarýna göre sýralamak için zamanlamayý düzenli olarak denetler ve 
                    çalýþanlarýn bunlarý yürütmesine olanak tanýr. 
                    Varsayýlan olarak, kontrol aralýðý 15 saniyeye eþittir, ancak BackgroundJobServer yapýcýsýna ilettiðiniz seçeneklerde 
                    SchedulePollingInterval özelliðini ayarlayarak deðiþtirebilirsiniz    */
                SchedulePollingInterval = TimeSpan.FromSeconds(30),

                //Arkaplanda çalýþacak Job sayýsýný deðiþtirebiliriz.
                WorkerCount = Environment.ProcessorCount * 5
            });

            // Tanýmlanan zaman diliminde sürekli çalýþtýðý için tetiklenmesine gerek yok, 
            // burada tanýmlayabiliriz. 
            app.UseCustomJobs();

        }
    }
}
