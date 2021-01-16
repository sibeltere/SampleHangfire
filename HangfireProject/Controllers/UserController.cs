using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangfireProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangfireProject.BackgroundJob.Schedules;

namespace HangfireProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        [Obsolete]
        public IActionResult GetUser()
        {
            UserJobs.GetUserList();
            return Ok($"Recurring Job Scheduled. Invoice will be mailed 2 minute for!");
        }

        
    }
}
