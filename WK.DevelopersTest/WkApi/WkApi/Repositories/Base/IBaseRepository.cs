using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WkApi.Repositories.Base
{
    /// <summary>
    /// IBaseRepository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Query all the entities from table
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Add Entity to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Update entity to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Delete entity from table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
