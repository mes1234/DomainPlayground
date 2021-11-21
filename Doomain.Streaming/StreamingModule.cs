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
            builder.RegisterType<RedisStreaming>().As<IStreaming>();
            builder.RegisterType<FileStreaming>().As<IStreaming>();
        }
    }
}
