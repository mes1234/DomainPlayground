using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shared;

namespace Events
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class StoreEventNotification : INotification
    {
        /// <summary>
        /// Gets the type of the event.</summary>
        /// <value>The type of the event.</value>
        public EventTypes EventType { get; init; }

        /// <summary>
        /// Gets the type of the content.</summary>
        /// <value>The type of the content.</value>
        public ContentTypes ContentType { get; init; }

        /// <summary>
        /// Gets the content.</summary>
        /// <value>The content.</value>
        public byte[] Content { get; init; }

        /// <summary>
        /// Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public Guid Id { get; init; }
    }
}
