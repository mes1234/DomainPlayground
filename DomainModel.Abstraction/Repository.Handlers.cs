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
    public partial class Repository<T>
         where T : IEvent, IEntity
    {
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

            if (!ChekIfRequiresUpdate((T)notification.Item)) return Task.CompletedTask;

            Repo.AddOrUpdate(notification.Item.Id, (T)notification.Item, (key, value) => (T)notification.Item);

            _logger.LogInformation("Added item to Repository from external source {@item} in revision {revision}", notification.Item, notification.Item.Revision);

            return Task.CompletedTask;
        }


        /// <summary>
        /// Check if Repository owned revision is not higher than incomming
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>status flag</returns>
        private bool ChekIfRequiresUpdate(T item)
        {
            var id = item.Id;
            var revision = item.Revision;

           if(! Repo.TryGetValue(id,out var owned)) return true;

           return owned.Revision <= revision;

        }
    }
}
