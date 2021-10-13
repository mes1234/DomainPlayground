using System;
using DomainModel.Abstraction;
using Events;
using Shared;

namespace DomainModel.A
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA : IEntity, IEvent
    {

        /// <summary>
        /// Create instance of ModelA
        /// </summary>
        public ModelA()
        {
            _id = Guid.NewGuid();
        }
    }
}
