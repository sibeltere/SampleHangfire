using HangfireProject.CoreLayer.EFBase;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.DataLayer.Context;
using HangfireProject.EntityLayer.DTO;
using HangfireProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace HangfireProject.DataLayer.Concrete
{
    public class UserCrendentialsDAL : EntityRepositoryBase<UserCredentials, HangfireDbContext>, IUserCredentialsDAL
    {
        public UserCrendentialsDTO GetUserCrendentials(string username, string password)
        {
            var model = new UserCrendentialsDTO();

            using (var dbContext = new HangfireDbContext())
            {


                model =
                       (from cre in dbContext.UserCredentials
                        join user in dbContext.User on cre.UserId equals user.Id
                        where (cre.Username == username) && (cre.Password == password)
                        select new UserCrendentialsDTO { Username = cre.Username, Password = cre.Password }).FirstOrDefault();



                return model;
            }
        }
    }
}
