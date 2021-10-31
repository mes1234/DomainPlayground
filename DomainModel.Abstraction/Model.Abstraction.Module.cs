using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doomain.Events;
using Doomain.Shared;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Doomain.Abstraction
{
    /// <summary>
    /// Registration of Abstraction modules
    /// </summary>
    public class ModelAbstractionModule : Module
    {
        /// <summary>
        /// Load items to DI container
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<ModelFactory>().As<IModelFactory>();
        }
    }
}
