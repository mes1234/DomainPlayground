using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;
using MediatR;

namespace Doomain.Events
{
    /// <summary>
    /// AddOrUpdateNotification
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <seealso cref="MediatR.INotification" />
    public class AddOrUpdateNotification<T> : INotification
        where T : IEvent
    {
        private readonly T _item;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateNotification{T}"/> class.
        /// </summary>
        /// <param name="item">item</param>
        public AddOrUpdateNotification(T item)
        {
            _item = item;
        }

        /// <summary>
        /// Gets inside element
        /// </summary>
        public T Item => _item;
    }
}
