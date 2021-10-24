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
        where T : IEvent, IEntity
    {
        private readonly T _item;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateNotification{T}"/> class.
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="dir">direction</param>
        public AddOrUpdateNotification(T item, Direction dir)
        {
            _item = item;
            Direction = dir;
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public Direction Direction { get; init; }

        /// <summary>
        /// Gets inside element
        /// </summary>
        public T Item => _item;
    }
}
