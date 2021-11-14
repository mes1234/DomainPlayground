using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doomain.Shared;
using MediatR;

namespace Doomain.Events
{
    /// <summary>
    /// Notify about removal
    /// </summary>
    public class RemoveNotification : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveNotification"/> class.
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="direction">direction</param>
        public RemoveNotification(IEvent item, Direction direction)
        {
            Item = item;
            Direction = direction;
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public Direction Direction { get; init; }

        /// <summary>
        /// Gets Id
        /// </summary>
        public IEvent Item { get; init; }
    }
}
