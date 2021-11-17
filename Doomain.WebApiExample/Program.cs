using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Doomain;
using Doomain.Example;
using Doomain.Shared;
using Doomain.Streaming;
using Doomain.WebApiExample.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper.Contrib.Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

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
                    builder.RegisterAutoMapper(typeof(ModelAProfile).Assembly);
                });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHostedService<NatsStreaming>();
builder.Services.AddHostedService<FileStreaming>();


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
