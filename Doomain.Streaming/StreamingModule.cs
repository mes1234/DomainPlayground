using Autofac;
using Microsoft.Extensions.Configuration;
using System.Configuration;

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
            switch (StreamingConfig.Mode)
            {
                case "File":
                    builder.RegisterType<FileStreaming>().As<IStreaming>();
                    break;
                case "Redis":
                    builder.RegisterType<RedisStreaming>().As<IStreaming>();
                    break;
            }
        }
    }
}
