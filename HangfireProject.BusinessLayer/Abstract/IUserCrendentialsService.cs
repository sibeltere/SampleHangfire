using HangfireProject.EntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.BusinessLayer.Abstract
{
    public interface IUserCrendentialsService
    {
        UserCrendentialsDTO GetUserCrendentialsDTO(string username, string password);
    }
}
