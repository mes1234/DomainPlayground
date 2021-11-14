using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doomain.Events;
using Doomain.Streaming;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Doomain.EventsDispatcher
{
    /// <summary>
    /// Registration of Abstraction modules
    /// </summary>
    public class EventsDispatcherModule : Module
    {
        /// <summary>
        /// Load items to DI container
        /// </summary>
        /// <param name="builder">builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddOrUpdateItemNotificationHandler>()
                .As<IStreamingHandler>();
            builder.RegisterType<RemoveItemNotificationHandler>()
                .As<IStreamingHandler>();

            builder.RegisterType<EventBuilder>().As<IEventBuilder>();
        }
    }
}
