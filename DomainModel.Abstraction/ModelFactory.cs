using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Doomain.Abstraction
{
    /// <inheritdoc cref="IModelFactory"/>
    public class ModelFactory : IModelFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelFactory"/> class.
        /// </summary>
        /// <param name="lifetimeScope">The lifetime scope.</param>
        public ModelFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <inheritdoc/>
        public T Create<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }
    }
}
