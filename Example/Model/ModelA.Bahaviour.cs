using System;
using Doomain.Shared;

namespace Example
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA : IEntity, IEvent
    {
        private readonly ICoder _coder;

        /// <summary>
        /// Create instance of ModelA
        /// </summary>
        public ModelA(ICoder coder)
        {
            _id = Guid.NewGuid();
            _coder = coder;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
