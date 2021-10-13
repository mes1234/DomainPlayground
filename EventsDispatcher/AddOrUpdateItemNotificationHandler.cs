using System;
using System.Threading;
using System.Threading.Tasks;
using Events;
using MediatR;
using Shared;

namespace EventsDispatcher
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="T">TODO</typeparam>
    public class AddOrUpdateItemNotificationHandler<T> : INotificationHandler<AddOrUpdateNotification<T>>
        where T : IEvent
    {
        private readonly ICoder _coder;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateItemNotificationHandler{T}"/> class.
        /// </summary>
        /// <param name="coder">coder</param>
        /// <param name="mediator">mediator</param>
        public AddOrUpdateItemNotificationHandler(ICoder coder, IMediator mediator)
        {
            _coder = coder;
            _mediator = mediator;
        }

        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.NotImplementedException">TODO</exception>
        public async Task Handle(AddOrUpdateNotification<T> notification, CancellationToken cancellationToken)
        {
            var item = notification.Item;

            await _mediator.Publish(new StoreEventNotification
            {
                Content = item.Serialize(_coder),
                ContentType = item.Type,
                EventType = EventTypes.AddedOrUpdated,
                Id = item.Id,
            }).ConfigureAwait(false);
        }
    }
}
