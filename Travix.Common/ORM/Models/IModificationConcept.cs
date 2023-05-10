namespace Travix.Common.ORM.Models
{
    /// <summary>
    /// This interface signed entities that are wanted to store modification information (who and when modified).
    /// </summary>
    public interface IModificationConcept
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        long? LastModifierUserId { get; set; }
    }
}
