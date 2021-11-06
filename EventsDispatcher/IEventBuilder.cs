using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doomain.Shared;

namespace Doomain.EventsDispatcher
{

    /// <summary>
    /// Event builder
    /// </summary>
    public interface IEventBuilder
    {
        /// <summary>
        /// Builds the event.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="content">The content.</param>
        /// <returns>IEvent</returns>
        public IEvent BuildEvent(byte[] header, byte[] content);
    }
}
