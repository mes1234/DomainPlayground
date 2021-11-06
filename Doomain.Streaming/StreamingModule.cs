using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Doomain.Streaming
{
    /// <summary>
    /// Registration of shared modules
    /// </summary>
    public class StreamingModule : Module
    {
        /// <summary>
        /// Load items to DI container
        /// </summary>
        /// <param name="builder">builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NatsStreaming>().As<IStreaming>();
        }
    }
}
