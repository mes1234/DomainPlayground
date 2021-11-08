using Autofac;
using Autofac.Extensions.DependencyInjection;
using Doomain;
using Doomain.Example;
using Doomain.Streaming;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication
    .CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
         .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
                {
                    // Register your own things directly with Autofac here. Don't
                    // call builder.Populate(), that happens in AutofacServiceProviderFactory
                    // for you.
                    builder.InstallDoomain();
                    builder.RegisterType<ModelA>();
                });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<NatsStreaming>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
