using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class FakeStreaming : BackgroundService, IStreaming
    {
        private readonly IEnumerable<IStreamingHandler> handlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeStreaming"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public FakeStreaming(IServiceProvider serviceProvider)
        {
            handlers = serviceProvider.GetServices<IStreamingHandler>();
        }

        /// <inheritdoc/>
        public Task Publish(Topic topic, byte[] header, byte[] content)
        {
            foreach (var handler in handlers.Where(x => x.SupportedTopic == topic))
            {
                handler.Handle(header, content);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
