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
using Microsoft.Extensions.Logging;

namespace Doomain.Abstraction
{
    /// <summary>
    /// Generic repository 
    /// </summary>
    /// <typeparam name="T">Type of items to store</typeparam>
    public partial class Repository<T> : INotificationHandler<AddOrUpdateNotification>, IRepository<T>
         where T : IEvent, IEntity
    {
        private static readonly ConcurrentDictionary<Guid, T> Repo = new();
        private readonly IMediator _mediator;
        private readonly ILogger<Repository<T>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">logger</param>
        public Repository(
            IMediator mediator,
            ILogger<Repository<T>> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Add or update item to repository
        /// </summary>
        /// <param name="item">item to be added/updated</param>
        public async Task AddOrUpdate(T item)
        {
            item.Revision++;

            Repo.AddOrUpdate(
              item.Id,
              item,
              (key, value) => item);

            _logger.LogInformation("Added item to Repository from internal source {@item} in revision {revision}", item, item.Revision);

            await _mediator.Publish(new AddOrUpdateNotification(item, Direction.Outbound)).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve item from repository
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns></returns>
        public T Get(Guid id)
        {
            _logger.LogInformation("Attempt to retrieve item from repository {id}", id);

            Repo.TryGetValue(id, out T value);

            return value ?? throw new KeyNotFoundException($"Key {id} was not found");
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
