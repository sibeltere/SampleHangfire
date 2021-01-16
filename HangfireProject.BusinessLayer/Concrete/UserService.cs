using HangfireProject.BusinessLayer.Abstract;
using HangfireProject.DataLayer.Abstract;
using HangfireProject.EntityLayer.DTO;
using System.Collections.Generic;

namespace HangfireProject.BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        public readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            this._userDal = userDal;
        }
        public List<UserDTO> GetUserList()
        {
            var response = _userDal.GetUserList();
            return response;
        }

        
    }
}
