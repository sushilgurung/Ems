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
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithUserName(string userName);
        UserAuthViewModel GetUserDetailsByUserName(string userName);
    }
}
