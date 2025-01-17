﻿using HangfireProject.CoreLayer.EFBase;
using HangfireProject.EntityLayer.DTO;
using HangfireProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.DataLayer.Abstract
{
    public interface IUserDal: IEntityRepositoryBase<User>
    {
        List<UserDTO> GetUserList();
        //UserDTO GetUser(string username, string password);
    }
}
