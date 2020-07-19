using Library.DataAcessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DatabaseEntities DatabaseEntities;
        private IUnitOfWork unitOfWork;

        //private DatabaseEntities DatabaseEntities
        //{
        //    get
        //    {
        //        return DatabaseEntities as DatabaseEntities;
        //    }

        //}
        //public Repository(IUnitOfWork unitOfWork)
        //{
        //    if (unitOfWork == null)
        //    {
        //        throw new ArgumentNullException("Unit of work");
        //    }
        //    _unitOfWork = unitOfWork;
        //    this.DatabaseEntities = _unitOfWork.Db;
        //}
        //public Repository(DbContext context)
        //{
        //    DatabaseEntities = context;
        //}
        public Repository()
        {
            DatabaseEntities = new DatabaseEntities();
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // we can use another method 
        //private DatabaseEntities _context;
        //private DbSet<TEntity> dbSet;
        //public Repository()
        //{
        //    _context = new DatabaseEntities();
        //    dbSet = _context.Set<TEntity>();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {

            return DatabaseEntities.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get Table with pagination
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize)
        {
            return (DatabaseEntities.Set<TEntity>().Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        /// <summary>
        /// Get Table with pagination
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, DateTime>> orderBy, int pageNo, int pageSize)
        {
            return (DatabaseEntities.Set<TEntity>().Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
    
            return DatabaseEntities.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DatabaseEntities.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DatabaseEntities.Set<TEntity>().SingleOrDefault(predicate);
        }
        public bool Exists(Expression<Func<TEntity, bool>> whereCondition)
        {
            return DatabaseEntities.Set<TEntity>().Any(whereCondition);
        }

        public int Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            return DatabaseEntities.Set<TEntity>().Where(whereCondition).Count();
        }

        public void Add(TEntity entity)
        {
            DatabaseEntities.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DatabaseEntities.Set<TEntity>().AddRange(entities);
        }


        public void Update(TEntity entity)
        {
            DatabaseEntities.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                DatabaseEntities.Entry(item).State = EntityState.Modified;
            }
        }

        public void Remove(TEntity entity)
        {
            DatabaseEntities.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DatabaseEntities.Set<TEntity>().RemoveRange(entities);
        }

        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return DatabaseEntities.Set<TEntity>().SqlQuery(query, parameters);
        }

        public int Complete()
        {
            return DatabaseEntities.SaveChanges();
        }

        public int SaveChanges()
        {
            return DatabaseEntities.SaveChanges();
        }

        //public IDatabaseTransaction BeginTransaction()
        //{
        //    return new DatabaseTransaction(DatabaseEntities);
        //}
        public IEnumerable<TEntity> GetdataFromSqlcommand(string command, SqlParameter[] parameter)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"EXECUTE {command}");
            strBuilder.Append(string.Join(",", parameter.ToList().Select(s => $" @{s.ParameterName}")));

            return DatabaseEntities.Set<TEntity>().SqlQuery(strBuilder.ToString(), parameter);
        }

        //When you expect a model back (async)
        public IList<T> ExecWithStoreProcedure<T>(string query, SqlParameter[] parameters)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"EXECUTE {query}");
            strBuilder.Append(string.Join(",", parameters.ToList().Select(s => $" @{s.ParameterName}")));
            return DatabaseEntities.Database.SqlQuery<T>(strBuilder.ToString(), parameters).ToList();
        }

        public IList<T> ExecuteAsListWithStoreProcedure<T>(string query)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"EXECUTE {query}");
            return DatabaseEntities.Database.SqlQuery<T>(strBuilder.ToString()).ToList();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DatabaseEntities != null)
                {
                    this.DatabaseEntities.Dispose();
                    //this.Context = null;
                }
            }
        }
    }
}
