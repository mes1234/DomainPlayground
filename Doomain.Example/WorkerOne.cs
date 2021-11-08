using System;
using System.Threading;
using System.Threading.Tasks;
using Doomain.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Doomain.Example
{
    /// <summary>
    /// Service of worker
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
    public class WorkerOne : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IModelFactory _modelFactory;
        private readonly IRepository<ModelA> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerOne"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="modelFactory">The model factory.</param>
        /// <param name="repository">The repository.</param>
        public WorkerOne(
            ILogger<WorkerOne> logger,
            IModelFactory modelFactory,
            IRepository<ModelA> repository)
        {
            _logger = logger;
            _modelFactory = modelFactory;
            _repository = repository;
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>Task</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var m1 = _modelFactory.Create<ModelA>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("WorkerOne running  ");

                    m1.SetName("Witek");
                    m1.SetGuid(Guid.Parse("0721c89a-1437-4906-af53-da4d3880da6f"));
                    await _repository.AddOrUpdate(m1);
                    await Task.Delay(1000 * 3600);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Worker error at: {time} {@ex}", DateTimeOffset.Now, ex);
                }
            }
        }
    }
}
