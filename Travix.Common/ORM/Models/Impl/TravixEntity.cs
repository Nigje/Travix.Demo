using System;
using System.Collections.Generic;
using System.Text;

namespace Travix.Common.ORM.Models.Impl
{
    public class TravixEntity<TPrimaryKey> : ITravixEntity, ICreationConcept, IModificationConcept
    {
        public TPrimaryKey Id { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
