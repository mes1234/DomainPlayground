using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doomain.Events;
using MediatR;

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
            builder.RegisterGeneric(typeof(AddOrUpdateItemNotificationHandler<>)).AsImplementedInterfaces();
            builder.RegisterType<StoreEventNotificationHandler>().As<INotificationHandler<StoreEventNotification>>();
        }
    }
}
