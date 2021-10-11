using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    /// <summary>
    /// Domain event
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets defintion of type of event
        /// </summary>
        public EventType Type { get; }

        /// <summary>
        /// Obtain serialized version of given event
        /// </summary>
        /// <param name="coder">coder for serialization</param>
        /// <returns>byte representation</returns>
        public byte[] Serilize(ICoder coder);
    }
}
