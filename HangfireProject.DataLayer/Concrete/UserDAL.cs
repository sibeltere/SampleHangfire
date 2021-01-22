
using HangfireProject.CoreLayer.EFBase;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.DataLayer.Context;
using HangfireProject.EntityLayer.DTO;
using HangfireProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangfireProject.DataLayer.Concrete
{
    public class UserDAL : EntityRepositoryBase<User, HangfireDbContext>, IUserDal
    {
        //public UserDTO GetUser(string username, string password)
        //{
        //    var model = new UserDTO();

        //    using (var dbContext = new HangfireDbContext())
        //    {
        //        var user = from us in dbContext.User.Where(x => x.Username == username && x.Password == password)
        //                   select us;
        //        if (user != null)
        //        {
        //            model = new UserDTO()
        //            {
        //                Name = user.FirstOrDefault().Name,
        //                Surname = user.FirstOrDefault().Surname
        //            };
        //        }
        //        return model;
        //    }

        //}

        public List<UserDTO> GetUserList()
        {
            using (var dbContext = new HangfireDbContext())
            {
                var userList = from user in dbContext.User
                               select new UserDTO()
                               {
                                   Name = user.Name,
                                   Surname = user.Surname
                               };

                return userList.ToList();
            }


        }
    }
}
