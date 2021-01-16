using HangfireProject.EntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.BusinessLayer.Abstract
{
    public interface IUserService
    {
        List<UserDTO> GetUserList();
    }
}
