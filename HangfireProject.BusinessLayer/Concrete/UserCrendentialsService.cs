using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.EntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.BusinessLayer.Concrete
{
    public class UserCrendentialsService : IUserCrendentialsService
    {
        public readonly IUserCredentialsDAL _userCrendentialsDAL;
        public UserCrendentialsService(IUserCredentialsDAL userCrendentialsDAL)
        {
            _userCrendentialsDAL = userCrendentialsDAL;
        }
        public UserCrendentialsDTO GetUserCrendentialsDTO(string username, string password)
        {
            return _userCrendentialsDAL.GetUserCrendentials(username, password);
        }
    }
}
