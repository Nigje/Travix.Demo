using Sepid.EKYC.Framework.Orm.Models;
using Sepid.EKYC.Framework.Orm.Models.Impl;
using Travix.Common.Models;
using Travix.Common.ORM.EntityFramework;

namespace Travix.Common.ORM.Models.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravixDBContext _db;
        private Dictionary<Type, object> _repositories;
        private readonly RequestContext _requestContext;
        public UnitOfWork(TravixDBContext dBContext, RequestContext requestContext)
        {
            _db = dBContext;
            _requestContext = requestContext;
            _repositories = new Dictionary<Type, object>();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public IRepository<Tout> GenericRepository<Tout>()
        {
            if (_repositories.ContainsKey(typeof(Tout)))
                return (IRepository<Tout>)_repositories[typeof(Tout)];
            _db.RequestContext = _requestContext;
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(Tout)), _db);
            _repositories.TryAdd(typeof(Tout), repositoryInstance);
            return (IRepository<Tout>)repositoryInstance;
        }
    }
}
