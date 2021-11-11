using Doomain.Example;
using Xunit;
using AutoMapper;
using System;
using Doomain.WebApiExample.Mappings;
using FluentAssertions;
using NSubstitute;
using Doomain.Shared;

namespace Doomain.WebApiExample.Tests
{
    public class Mappings_Tests
    {
        protected IMapper mapper;
        protected ICoder coder;

        public Mappings_Tests()
        {
             coder = Substitute.For<ICoder>();
             mapper = new Mapper(new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new ModelAProfile(coder));
                }));
        }

        [Fact]
        public void FromDtoToModel_Test()
        {
            var dto = new Modeladto
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Revision = 44,
            };

            

            var model = mapper.Map<ModelA>(dto);

            model.Name.Should().Be(dto.Name);
            model.Revision.Should().Be(dto.Revision);
            model.Id.Should().Be(dto.Id);
        }

        [Fact]
        public void FromModelToDto_Test()
        {
            var model = new ModelA(coder);

            model.SetName("test");
            model.SetGuid(Guid.NewGuid());
            model.Revision=5454;

            var dto = mapper.Map<Modeladto>(model);

            dto.Name.Should().Be(model.Name);
            dto.Revision.Should().Be(model.Revision);
            dto.Id.Should().Be(model.Id);

        }
    }
}