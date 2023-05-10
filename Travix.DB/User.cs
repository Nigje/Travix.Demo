using Travix.Common.ORM.Models.Impl;

namespace Travix.DB
{
    public class User: TravixEntity<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}