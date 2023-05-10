using System.Linq.Expressions;


namespace Sepid.EKYC.Framework.Orm.Models
{
    public interface IRepository<IEkycEntity>
    {
        Task AddAsync(IEkycEntity entity);
        Task AddRangeAsync(IEnumerable<IEkycEntity> entities);
        void Add(IEkycEntity entity);
        void AddRange(IEnumerable<IEkycEntity> entities);
        void Remove(IEkycEntity entity);
        void RemoveRange(IEnumerable<IEkycEntity> entities);


        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>Only one entity</returns>
        Task<IEkycEntity> SingleAsync(Expression<Func<IEkycEntity, bool>> predicate);

        /// <summary>
        /// Get collection of entities that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        IQueryable<IEkycEntity> Where(Expression<Func<IEkycEntity, bool>> predicate,
            params Expression<Func<IEkycEntity, object>>[] includeEntities);

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false</returns>
        Task<bool> AnyAsync(Expression<Func<IEkycEntity, bool>> criteria);

        /// <summary>
        /// Gets count based on given criteria
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>count of entities</returns>
        Task<int> CountAsync(Expression<Func<IEkycEntity, bool>> criteria);

        /// <summary>
        /// Gets count
        /// </summary>
        /// <returns>count of entities</returns>
        Task<int> CountAsync();

        /// <summary>
        /// First or null if no default
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <param name="orderProperty">lambda expression to orderBy property</param>
        /// <param name="orderAsc">order asc or desc</param>
        /// <param name="includeProperties">Entities to include</param>
        /// <returns>IEkycEntity</returns>
        Task<IEkycEntity> FirstOrDefaultAsync(Expression<Func<IEkycEntity, bool>> predicate,
            Expression<Func<IEkycEntity, object>> orderProperty, bool orderAsc,
            params Expression<Func<IEkycEntity, object>>[] includeProperties);

        /// <summary>
        /// First or null if no default
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <param name="includeProperties">Entities to include</param>
        /// <returns>Entity</returns>
        Task<IEkycEntity> FirstOrDefaultAsync(Expression<Func<IEkycEntity, bool>> predicate,
            params Expression<Func<IEkycEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets first entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeEntities"></param>
        /// <returns></returns>
        Task<IEkycEntity> FirstAsync(Expression<Func<IEkycEntity, bool>> predicate,
            params Expression<Func<IEkycEntity, object>>[] includeEntities);

        /// <summary>
        /// Deletes entity or entities from the context based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        Task RemoveAsync(Expression<Func<IEkycEntity, bool>> predicate);
    }
}
