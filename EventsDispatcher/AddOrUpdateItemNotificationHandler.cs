using System;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Abstraction;
using Events;
using MediatR;

namespace EventsDispatcher
{
    public class AddOrUpdateItemNotificationHandler<T> : INotificationHandler<AddOrUpdateNotification<T>>
        where T : IEvent
    {
        public Task Handle(AddOrUpdateNotification<T> notification, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
