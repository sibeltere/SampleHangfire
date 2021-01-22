using HangfireProject.BackgroundJob.Schedules;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireProject.Configurations
{
    public static class JobConfigurations
    {
        public static void UseCustomJobs(this IApplicationBuilder app)
        {
            UserJobs.GetUserList();
        }
    }
}
