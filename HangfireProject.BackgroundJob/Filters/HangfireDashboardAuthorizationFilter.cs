using Hangfire.Annotations;
using Hangfire.Dashboard;
using HangfireProject.BackgroundJob.Schedules;
using HangfireProject.DataLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HangfireProject.BackgroundJob.Filters
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {

      
        public HangfireDashboardAuthorizationFilter()
        {
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            //var password = httpContext.User.FindFirst(ClaimTypes.Hash)?.Value;
            //var username = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            //{
            //   var user= _userDal.GetUser(username, password);

            //    if (user != null)
            //        return true;

            //    else
            //        return false;
            //}
            //if (httpContext.User.Identity.IsAuthenticated)
            //{
            //    UserJobs.GetUserList();
            //}

            return httpContext.User.Identity.IsAuthenticated;
            //return false;
        }



    }
}
