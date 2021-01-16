using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.EntityLayer.DTO;
using System.Collections.Generic;

namespace HangfireProject.BackgroundJob.Managers.UserJobs
{
    public class UserScheduleJobManager
    {
        private readonly IUserService _userService;

        public UserScheduleJobManager(IUserService userService)
        {
            this._userService = userService;
        }

        public List<UserDTO> GetUserList()
        {
            return _userService.GetUserList();  
        }

    }
}
