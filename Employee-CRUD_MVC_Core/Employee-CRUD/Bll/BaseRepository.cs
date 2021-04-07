using Employee_CRUD.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Employee_CRUD.Bll
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

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private DbSet<T> _entities;
        string errorMessage = string.Empty;

        public BaseRepository(DataContext context)
        {
            this._dbContext = context;
            _entities = context.Set<T>();
        }

        public DbContext DBContext
        {
            get { return _dbContext; }
        }
        #region Read Methods
        /// <summary>
        ///  Finds an entity with the given primary key values. If an entity with the given
        ///     primary key values is being tracked by the context, then it is returned immediately
        ///     without making a request to the database. Otherwise, a query is made to the database
        ///     for an entity with the given primary key values and this entity, if found, is
        ///     attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id">The values of the primary key for the entity to be found.</param>
        /// <returns> The entity found, or null.</returns>

        public virtual T Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        /// <summary>
        /// Retuns those entities who's are match with filter criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Where(predicate);
            return query;
        }

        /// <summary>
        /// Retuns First entity who's are match with filter criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var first = FindBy(predicate).FirstOrDefault();
            return first;
        }

        /// <summary>
        /// Retuns last entity who's are match with filter criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T LastOrDefault(Expression<Func<T, bool>> predicate)
        {
            var last = FindBy(predicate).LastOrDefault();
            return last;
        }

        /// <summary>
        /// checking any entity is exist with filter criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return FindBy(predicate).Any();
        }

        #endregion

        #region Write Methods


        public T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return _entities.Add(entity).Entity;
        }

        public void Edit(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> enetities)
        {
            _entities.RemoveRange(enetities);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }


        #endregion
    }
}
