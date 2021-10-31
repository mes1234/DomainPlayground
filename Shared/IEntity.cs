using System;

namespace Doomain.Shared
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

        /// <summary>
        /// Gets or sets the revisison.
        /// </summary>
        /// <value>
        /// The revisison.
        /// </value>
        public int Revision { get; set; }
    }
}
