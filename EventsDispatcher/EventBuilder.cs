using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;

namespace Doomain.EventsDispatcher
{

    /// <inheritdoc/>
    public class EventBuilder : IEventBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICoder _coder;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBuilder"/> class.
        /// </summary>
        /// <param name="serviceProvider">serviceProvider</param>
        /// <param name="coder">coder</param>
        public EventBuilder(
            IServiceProvider serviceProvider,
            ICoder coder)
        {
            _serviceProvider = serviceProvider;
            _coder = coder;
        }

        /// <inheritdoc/>
        public IEvent BuildEvent(byte[] header, byte[] content)
        {
            var storeEventNotification = new StoreEventNotification(_coder, header, content);

            var obj = (IEvent)_serviceProvider.GetService(storeEventNotification.ContentType);

            obj.Deserialize(content);

            return obj;
        }
    }
}
