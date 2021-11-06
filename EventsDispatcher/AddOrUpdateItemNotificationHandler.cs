using System;
using System.Threading;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;
using Doomain.Streaming;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Doomain.EventsDispatcher
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class AddOrUpdateItemNotificationHandler : INotificationHandler<AddOrUpdateNotification>, IStreamingHandler
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AddOrUpdateItemNotificationHandler> _logger;
        private readonly IEventBuilder _eventBuilder;
        private readonly ICoder _coder;


        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateItemNotificationHandler"/> class.
        /// </summary>
        /// <param name="eventBuilder">eventBuilder</param>
        /// <param name="mediator">mediator</param>
        /// <param name="logger">logger</param>
        /// <param name="coder">coder</param>
        public AddOrUpdateItemNotificationHandler(
            IEventBuilder eventBuilder,
            IMediator mediator,
            ILogger<AddOrUpdateItemNotificationHandler> logger,
            ICoder coder)
        {
            _eventBuilder = eventBuilder;
            _mediator = mediator;
            _logger = logger;
            _coder = coder;
        }

        /// <inheritdoc/>
        public Topic SupportedTopic => Topic.AddOrUpdated;

        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task Handle(AddOrUpdateNotification notification, CancellationToken cancellationToken)
        {
            if (notification.Direction == Direction.Inbound) return;

            _logger.LogInformation("Handling Outbound {@notification} in revision {revision}", notification, notification.Item.Revision);

            var item = notification.Item;

            await _mediator.Publish(new StoreEventNotification(_coder)
            {
                Content = item.Serialize(),
                ContentType = item.Type,
                EventType = EventTypes.AddedOrUpdated,
                Id = item.Id,
            }).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Status> Handle(byte[] header, byte[] content)
        {
            try
            {
                IEvent obj = _eventBuilder.BuildEvent(header, content);

                _logger.LogInformation("Handling Inbound {@obj} in revision {revision}", obj, obj.Revision);

                await _mediator
                    .Publish(new AddOrUpdateNotification(obj, Direction.Inbound))
                    .ConfigureAwait(false);

                return Status.Ack;
            }
            catch (Exception)
            {
                return Status.Nack;
            }
        }
    }
}
