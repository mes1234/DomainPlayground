using System;
using System.Threading.Tasks;
using Autofac;
using DomainModel.A;
using DomainModel.Abstraction;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(ModelAbstractionModule).Assembly);
            builder.RegisterType<Worker>();
            var worker = builder.Build().Resolve<Worker>();

            await worker.Run();

        }
    }


    public class Worker
    {
        private readonly IRepository<ModelA> _repository;
        public Worker(IRepository<ModelA> repository)
        {
            _repository = repository;
        }

        public async Task Run()
        {
            var m1 = new ModelA();
            await _repository.AddOrUpdate(m1);

            var m2 = await _repository.Get(m1.Id);
        }
    }
}
