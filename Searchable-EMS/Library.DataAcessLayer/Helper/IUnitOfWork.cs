using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAcessLayer.Repository;
namespace Library.DataAcessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Db { get; }

      
        // IUserRepository User { get; }
        // int Complete();
    }
}

