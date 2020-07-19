using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Entities;
using Library.DataAcessLayer.Repository;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public DatabaseEntities DbContext
        {
            get
            {
                return DatabaseEntities as DatabaseEntities;
            }
        }
   
        public User GetUserWithUserName(string userName)
        {
            return DbContext.User.SingleOrDefault(x => x.UserName == userName);
        }

        public UserAuthViewModel GetUserDetailsByUserName(string userName)
        {
            UserAuthViewModel userdetails = DbContext.User.Where(x => x.UserName == userName && x.IsDeleted == false).
                    Select(t => new
                    {
                        UserId = t.UserId,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        UserName = t.UserName,
                        Email = t.Email,
                        Password = t.Password,
                        PasswordFormat = t.PasswordFormat,
                        PasswordSalt = t.PasswordSalt,
                        RoleId = DbContext.UserInRoles.Where(x => x.UserId == t.UserId).Select(m => m.RoleId),
                        CreatedBy = t.CreatedBy,
                        IsActive = t.IsActive,
                        IsDeleted = t.IsDeleted,
                    }).
                    Select(u => new UserAuthViewModel
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email,
                        Password = u.Password,
                        PasswordFormat = u.PasswordFormat,
                        PasswordSalt = u.PasswordSalt,
                        RoleId = u.RoleId.Select(x => x).ToList(),
                        CreatedBy = u.CreatedBy,
                        IsActive = u.IsActive,
                        IsDeleted = u.IsDeleted,
                    }).FirstOrDefault();
            return userdetails;
        }
    }
}
