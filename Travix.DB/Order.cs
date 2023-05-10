using System.ComponentModel.DataAnnotations.Schema;
using Travix.Common.ORM.Models.Impl;

namespace Travix.DB
{
    public class Order : TravixEntity<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
