using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using HangfireProject.BackgroundJob.Schedules;
using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.BusinessLayer.Concrete;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.DataLayer.Concrete;
using HangfireProject.DataLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddControllers();

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
            services.AddScoped<IUserDal, UserDAL>();
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

            app.UseHangfireDashboard("/myhangfire", new DashboardOptions
            {
                DashboardTitle = "Sibel Tere Hangfire DashBoard",  // Dashboard sayfas�na ait Ba�l�k alan�n� de�i�tiririz.
                AppPath = "/Home/HangfireAbout",                     // Dashboard �zerinden "back to site" button
                //Authorization = new[] { new HangfireDashboardAuthorizationFilter() },   // G�venlik i�in Authorization ��lemleri
            });

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                /*  Hangfire Server, planlanan i�leri s�ralar�na g�re s�ralamak i�in zamanlamay� d�zenli olarak denetler ve 
                    �al��anlar�n bunlar� y�r�tmesine olanak tan�r. 
                    Varsay�lan olarak, kontrol aral��� 15 saniyeye e�ittir, ancak BackgroundJobServer yap�c�s�na iletti�iniz se�eneklerde 
                    SchedulePollingInterval �zelli�ini ayarlayarak de�i�tirebilirsiniz    */
                SchedulePollingInterval = TimeSpan.FromSeconds(30),

                //Arkaplanda �al��acak Job say�s�n� de�i�tirebiliriz.
                WorkerCount = Environment.ProcessorCount * 5
            });

            // Tan�mlanan zaman diliminde s�rekli �al��t��� i�in tetiklenmesine gerek yok, 
            // burada tan�mlayabiliriz. tan�mlayabiliriz.  
            UserJobs.GetUserList();
        }
    }
}