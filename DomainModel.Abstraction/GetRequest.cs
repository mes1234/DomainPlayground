using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DomainModel.Abstraction
{
    /// <summary>
    /// Request for item
    /// </summary>
    public class GetRequest : IRequest<IEntity>
    {
        private readonly Guid _id;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public GetRequest(Guid id)
        {
            _id = id;
        }
    }
}
