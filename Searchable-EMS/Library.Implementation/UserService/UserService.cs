using AutoMapper;
using Library.DataAcessLayer.Repository;
using Library.DataAcessLayer.Entities;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core;
using Library.Core.Common;

namespace Library.Implementation
{
    public class UserService : IUserService
    {
        Utilities web = new Utilities();
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public UserService()
        {

        }

        public UserModel GetUserWithUserName(string userName)
        {
            User user = new User()
            {
                Id = 1,
                UserId = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "SuperAdmin",
                Password = "1234",
                PasswordFormat = 1,
                PasswordSalt = "",
                Email = "adminsupport@yopmail.com",
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                // CreatedBy = "SuperAdmin"
            };
            //_userRepository.GetUserWithUserName(userName);
            //MapperWrapper.Mapper.Map<UserModel>(user);
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>());
            //var mapper = config.CreateMapper();
            // AutoMapperConfig.Configure();
            UserModel userDetail = new UserModel();
            return userDetail;
        }



    }
}
