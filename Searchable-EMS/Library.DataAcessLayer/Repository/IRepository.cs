using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize);
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, DateTime>> orderBy, int pageNo, int pageSize);
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        bool Exists(Expression<Func<TEntity, bool>> whereCondition);
        int Count(Expression<Func<TEntity, bool>> whereCondition);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void UpdateRange(List<TEntity> entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
        int Complete();
        int SaveChanges();
        IEnumerable<TEntity> GetdataFromSqlcommand(string command, SqlParameter[] parameter);
        IList<T> ExecWithStoreProcedure<T>(string query, SqlParameter[] parameters);
        IList<T> ExecuteAsListWithStoreProcedure<T>(string query);
    }
}
