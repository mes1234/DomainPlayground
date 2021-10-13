using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
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
        /// <param name="coder">coder for serialization</param>
        /// <returns>byte representation</returns>
        public byte[] Serialize(ICoder coder);
    }
}
