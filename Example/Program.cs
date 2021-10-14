using System;
using System.Threading.Tasks;
using Autofac;
using Doomain.Abstraction;
using Doomain.EventsDispatcher;
using Doomain.Shared;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(ModelAbstractionModule).Assembly);
            builder.RegisterAssemblyModules(typeof(EventsDispatcherModule).Assembly);
            builder.RegisterAssemblyModules(typeof(SharedModule).Assembly);
            builder.RegisterType<Worker>();
            builder.RegisterType<ModelA>();
            var worker = builder.Build().Resolve<Worker>();

            await worker.Run();

        }
    }


    public class Worker
    {
        private readonly IModelFactory _modelFactory;
        private readonly IRepository<ModelA> _repository;
        public Worker(
            IModelFactory modelFactory,
            IRepository<ModelA> repository)
        {
            _modelFactory = modelFactory;
            _repository = repository;
        }

        public async Task Run()
        {
            var m1 = _modelFactory.Create<ModelA>();
            m1.SetName("Witek");
            await _repository.AddOrUpdate(m1);

            //var m2 = await _repository.Get(m1.Id);
        }
    }
}
