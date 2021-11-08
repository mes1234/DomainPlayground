using Autofac;
using Doomain.Abstraction;
using Doomain.EventsDispatcher;
using Doomain.Shared;
using Doomain.Streaming;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain
{
    /// <summary>
    /// Support for installation
    /// </summary>
   public static class Installation
    {
        /// <summary>
        /// Install doomain in autofac container
        /// </summary>
        /// <param name="builder">builder</param>
        public static void InstallDoomain(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(typeof(ModelAbstractionModule).Assembly);
            builder.RegisterAssemblyModules(typeof(EventsDispatcherModule).Assembly);
            builder.RegisterAssemblyModules(typeof(SharedModule).Assembly);
            builder.RegisterAssemblyModules(typeof(StreamingModule).Assembly);
            builder.RegisterMediatR(typeof(ModelAbstractionModule).Assembly);
            builder.RegisterMediatR(typeof(EventsDispatcherModule).Assembly);
            builder.InstallRepositiories();
        }

        private static void InstallRepositiories(this ContainerBuilder containerBuilder)
        {
           

            var modelTypes = GetAllTypesThatImplementInterface();
            foreach (var modelType in modelTypes)
                containerBuilder.RegisterType(typeof(Repository<>)
                       .MakeGenericType(modelType))
                       .As(typeof(Repository<>).MakeGenericType(modelType));
        }


        private static IEnumerable<Type> GetAllTypesThatImplementInterface()
        {
            var types = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                types.AddRange( assembly.GetTypes().Where(Check));

            return types;
        }

        private static bool Check(Type type)
        {
            return (typeof(IEntity).IsAssignableFrom(type) && !type.IsInterface);
        }
    }
}
