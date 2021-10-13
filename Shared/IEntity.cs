using System;

namespace Shared
{
    /// <summary>
    /// Entity is an object identified by its Id
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets Id of given entity
        /// </summary>
        public Guid Id { get; }
    }
}
