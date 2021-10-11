using System;
using DomainModel.Abstraction;

namespace DomainModel.A
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public class ModelA : IEntity
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
    }
}
