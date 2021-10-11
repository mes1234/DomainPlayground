using System;
using DomainModel.Abstraction;
using Events;

namespace DomainModel.A
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public class ModelA : IEntity, IEvent
    {
        private readonly Guid _id;

        /// <summary>
        /// Create instance of ModelA
        /// </summary>
        public ModelA()
        {
            _id = Guid.NewGuid();
        }
        /// <inheritdoc/>
        public Guid Id => _id;

        /// <inheritdoc/>
        public EventType Type => EventType.ModelA;


        /// <inheritdoc/>
        public byte[] Serialize(ICoder coder)
        {
            return coder.GetBytes(this);
        }
    }
}
