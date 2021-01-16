using System;
using Hangfire;
using HangfireProject.BackgroundJob.Managers.UserJobs;

namespace HangfireProject.BackgroundJob.Schedules
{
    public static class UserJobs
    {
        public static void GetUserList()
        {
            RecurringJob.AddOrUpdate<UserScheduleJobManager>(nameof(UserScheduleJobManager),
               job => job.GetUserList(), "*/2 * * * *");
        }
    }
}
