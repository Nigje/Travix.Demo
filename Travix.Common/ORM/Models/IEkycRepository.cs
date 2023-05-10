namespace Travix.Common.ORM.Models
{
    public interface ITravixRepository
    {
        /// <summary>
        /// Commit tracked objects
        /// </summary>
        /// <returns></returns>
        Task<long> CommitAsync();
    }
}
