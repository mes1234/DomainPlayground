using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using MediatR;

namespace DomainModel.Abstraction
{
    /// <summary>
    /// Notify about added or updated event
    /// </summary>
    public class AddOrUpdateNotification<T> : INotification
        where T : IEvent
    {
        private readonly T _item;

        /// <summary>
        /// Get inside element
        /// </summary>
        public T Item => _item;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public AddOrUpdateNotification(T item)
        {
            _item = item;
        }
    }


}
