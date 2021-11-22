using System;
using System.Linq;
using System.Threading.Tasks;
using WkApi.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace WkApi.Repositories.Base
{
    /// <summary>
    /// Generic Base Repository Decorator with support of CRUD operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly WkApiDbContext _appDbContext;
        private DbSet<TEntity> _table = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        public BaseRepository(WkApiDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _table = _appDbContext.Set<TEntity>();
        }

        /// <summary>
        /// Query to get all items
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _table.AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Add new entity to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _appDbContext.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }

        }

        /// <summary>
        /// update entity to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _appDbContext.Update(entity);
                await _appDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
        /// <summary>
        /// Delete entity from table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _table.Remove(entity);
                await _appDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
