﻿using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task Publish(Topic topic, byte[] msg)
        {
            try
            {
                using IConnection c = new ConnectionFactory().CreateConnection();

                IJetStream js = c.CreateJetStreamContext();

                await js.PublishAsync(topic.GetTopic(), msg).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IConnection c = new ConnectionFactory().CreateConnection();

                var pso = PushSubscribeOptions
                    .Builder()
                    .WithDurable("Dummy")
                    .Build();

                var js = c.CreateJetStreamContext();

                var sub = js.PushSubscribeAsync("generic.addedorupdated", MyHandler, true, pso);

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000).ConfigureAwait(false);
                }

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        private bool RecieveMessage(byte[] msg, out byte[] header, out byte[] content)
        {
            try
            {
                byte[] buf = new byte[msg.Length];

                using var ms = new MemoryStream(msg);

                ms.Read(buf, 0, 4);
                var headerSize = BitConverter.ToInt32(buf.AsSpan()[0..4]);

                ms.Read(buf, 0, 4);
                var contentSize = BitConverter.ToInt32(buf.AsSpan()[0..4]);

                header = new byte[headerSize];
                content = new byte[contentSize];

                ms.Read(header, 0, headerSize);
                ms.Read(content, 0, contentSize);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        private void MyHandler(object sender, MsgHandlerEventArgs args)
        {
            if (!RecieveMessage(args.Message.Data, out var header, out var content)) return;

            var topic = GetTopic(args.Message.Subject);

            foreach (var handler in _handlers.Where(x => x.SupportedTopic == topic))
            {
                _logger.LogInformation("Handling data for topic {@topic}", topic);
                handler.Handle(header, content);
            }
        }

        private Topic GetTopic(string natsSubject)
        {
            var subject = natsSubject.Split(".")[1];

            return subject switch
            {
                "addedorupdated" => Topic.AddOrUpdated,
                _ => throw new NotSupportedException($"subject {subject} is not supported"),
            };
        }
    }
}