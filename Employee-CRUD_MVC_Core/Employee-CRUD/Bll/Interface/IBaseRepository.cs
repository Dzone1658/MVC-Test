using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Bll.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        DbContext DBContext { get; }

        IEnumerable<T> GetAll();

        T Get(int id);

        T Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        int Save();

        Task<int> SaveAsync();

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        T LastOrDefault(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);
    }
}