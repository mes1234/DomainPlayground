using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Doomain.Events
{
    /// <summary>
    /// Notify about removal
    /// </summary>
    public class RemoveNotification : INotification
    {
        private readonly Guid _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveNotification"/> class.
        /// </summary>
        /// <param name="id">id</param>
        public RemoveNotification(Guid id)
        {
            _id = id;
        }
    }
}
