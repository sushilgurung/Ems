using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Repository;
namespace Library.DataAcessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseEntities _dbContext;
        //public UnitOfWork(DatabaseContext dbContext)
        //{
        //    _dbContext = dbContext;
        //    User = new UserRepository(_dbContext);
        //}
        public UnitOfWork()
        {
            _dbContext = new DatabaseEntities();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        //public IUserRepository User { get; set; }
        //public int Complete()
        //{
        //    return _dbContext.SaveChanges();
        //}

        public void Dispose() => _dbContext.Dispose();
    }
}
