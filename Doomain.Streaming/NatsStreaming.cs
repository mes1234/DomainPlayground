using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client;
using NATS.Client.JetStream;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class NatsStreaming : BackgroundService, IStreaming
    {
        private readonly IEnumerable<IStreamingHandler> _handlers;
        private readonly ILogger<NatsStreaming> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NatsStreaming"/> class.
        /// </summary>
        /// <param name="handlers">handlers.</param>
        /// <param name="logger">logger</param>
        public NatsStreaming(
            IEnumerable<IStreamingHandler> handlers,
            ILogger<NatsStreaming> logger)
        {
            _handlers = handlers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Publish(Topic topic, byte[] header, byte[] content)
        {
            try
            {
                using IConnection c = new ConnectionFactory().CreateConnection();

                IJetStream js = c.CreateJetStreamContext();

                js.Publish(topic.GetTopic(), header);

                return Task.CompletedTask;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IConnection c = new ConnectionFactory().CreateConnection();
            var js = c.CreateJetStreamContext();


            var sub =
            js.PushSubscribeAsync("generic.addedorupdated", MyHandler, false);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000).ConfigureAwait(false);
            }

            return;
        }

        private void MyHandler(object sender, MsgHandlerEventArgs args)
        {
            var header = args.Message.Data;
            var content = args.Message.Data;
            var topic = Topic.AddOrUpdated;
            foreach (var handler in _handlers.Where(x => x.SupportedTopic == topic))
            {
                _logger.LogInformation("Handling data for topic {@topic}", topic);
                handler.Handle(header, content);
            }
        }
    }
}
