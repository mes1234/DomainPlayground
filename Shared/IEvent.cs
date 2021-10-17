using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
{
    /// <summary>
    /// Domain event
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets defintion of type of event
        /// </summary>
        public ContentTypes Type { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Obtain serialized version of given event
        /// </summary>
        /// <returns>byte representation</returns>
        public byte[] Serialize();

        /// <summary>
        /// Deserializes the specified byte.
        /// </summary>
        /// <param name="content">The byte.</param>
        public void Deserialize(byte[] content);
    }
}
