using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

/*
 * Created by: Jahangir
 * Date created: 17.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of IRepository interface.
    /// </summary>
    /// <typeparam name="T">T is a model class.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EITTDbContext context;
        public Repository(EITTDbContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            try
            {
                return context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add multi object to database
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Returns a row from the table as an object if primary key matches.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Retrieves object.</returns>
        public virtual T Get(Guid id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a row from the table as an object if primary key matches.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Retrieves object.</returns>
        public virtual T Get(int id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns all rows as a list of objects from the table.
        /// </summary>
        /// <returns>List of objects.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return context.Set<T>()
                .AsQueryable()
                .AsNoTracking()
                .ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderByDescending(order)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of objects</returns>
        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order, Expression<Func<T, object>> obj)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .Where(predicate)
                    .OrderByDescending(order)
                    .Include(obj)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order, Expression<Func<T, object>> obj, Expression<Func<T, object>> next)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .OrderByDescending(order)
                    .Include(obj)
                    .Include(next)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order, Expression<Func<T, object>> obj, Expression<Func<T, object>> next, Expression<Func<T, object>> obj2)
        {
            try
            {
                return await context.Set<T>()
                      .AsQueryable()
                      .Where(predicate)
                      .OrderByDescending(order)
                      .Include(obj)
                      .Include(next)
                      .Include(obj2)
                      .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .Include(obj)
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .Include(obj)
                    .Include(next)
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Include child a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public async Task<T> LoadWithChildWithOrderByAsync<TEntity>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> LoadWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }
            if (orderBy != null)
                query = orderBy(query);

            return await query.Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Updates a row in the table.
        /// </summary>
        /// <param name="entity">Object to be updated.</param>
        public virtual void Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                context.Set<T>().Update(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public virtual void Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}