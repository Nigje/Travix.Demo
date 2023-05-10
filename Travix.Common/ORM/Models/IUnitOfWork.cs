using Sepid.EKYC.Framework.Orm.Models;

namespace Travix.Common.ORM.Models
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IRepository<Tout> GenericRepository<Tout>();
    }
}
