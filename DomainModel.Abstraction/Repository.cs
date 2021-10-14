using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Repository<T> : IRepository<T>
        where T : IEntity, IEvent
    {
        private readonly IMediator _mediator;

        public Repository(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddOrUpdate(T item)
        {
            await _mediator.Publish(new AddOrUpdateNotification<T>(item)).ConfigureAwait(false);
        }


        public async Task<T> Get(Guid id)
        {
            return (T)await _mediator.Send(new GetRequest(id));
        }

        public async Task TryRemove(Guid id)
        {
            await _mediator.Publish(new RemoveNotification(id));
        }

    }
}
