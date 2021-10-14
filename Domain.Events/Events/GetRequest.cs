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
    /// Request for item
    /// </summary>
    public class GetRequest : IRequest<IEntity>
    {
        private readonly Guid _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequest"/> class.
        /// </summary>
        /// <param name="id">id</param>
        public GetRequest(Guid id)
        {
            _id = id;
        }
    }
}
