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
    public class StoreEventNotificationHandler : INotificationHandler<StoreEventNotification>
    {
        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.NotImplementedException">TODO</exception>
        public Task Handle(StoreEventNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
