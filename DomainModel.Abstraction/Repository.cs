using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;
using MediatR;

namespace Doomain.Abstraction
{
    /// <summary>
    /// Generic repository 
    /// </summary>
    /// <typeparam name="T">Type of items to store</typeparam>
    public class Repository<T> : INotificationHandler<AddOrUpdateNotification>, IRepository<T>
         where T : IEvent, IEntity
    {
        private readonly static ConcurrentDictionary<Guid, T> Repo = new();
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public Repository(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add or update item to repository
        /// </summary>
        /// <param name="item">item to be added/updated</param>
        public async Task AddOrUpdate(T item)
        {
            await _mediator.Publish(new AddOrUpdateNotification(item, Direction.Outbound)).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve item from repository
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns></returns>
        public async Task<T> Get(Guid id)
        {
            return (T)await _mediator.Send(new GetRequest(id));
        }

        /// <summary>
        /// Handles a notification
        /// </summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public Task Handle(AddOrUpdateNotification notification, CancellationToken cancellationToken)
        {
            // Repository should only take care of inbound notification
            if (notification.Direction == Direction.Outbound) return Task.CompletedTask;

            Repo[notification.Item.Id] = (T)notification.Item;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Try to remove item from repository
        /// </summary>
        /// <param name="id">Id of item</param>
        public async Task TryRemove(Guid id)
        {
            await _mediator.Publish(new RemoveNotification(id));
        }


    }
}
