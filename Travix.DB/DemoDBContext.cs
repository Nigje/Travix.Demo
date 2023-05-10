using Microsoft.EntityFrameworkCore;
using Travix.Common.Models;
using Travix.Common.ORM.EntityFramework;

namespace Travix.DB
{
    public class DemoDBContext : TravixDBContext
    {
        public DemoDBContext(DbContextOptions<DemoDBContext> options, RequestContext requestContext) : base(options, requestContext)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
