using System;
using System.Threading;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;
using MediatR;

namespace Doomain.EventsDispatcher
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="T">TODO</typeparam>
    public class AddOrUpdateItemNotificationHandler : INotificationHandler<AddOrUpdateNotification>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateItemNotificationHandler"/> class.
        /// </summary>
        /// <param name="mediator">mediator</param>
        public AddOrUpdateItemNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.NotImplementedException">TODO</exception>
        public async Task Handle(AddOrUpdateNotification notification, CancellationToken cancellationToken)
        {
            var item = notification.Item;

            await _mediator.Publish(new StoreEventNotification
            {
                Content = item.Serialize(),
                ContentType = item.Type,
                EventType = EventTypes.AddedOrUpdated,
                Id = item.Id,
            }).ConfigureAwait(false);
        }
    }
}
