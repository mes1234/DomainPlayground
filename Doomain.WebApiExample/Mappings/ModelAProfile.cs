using AutoMapper;
using Doomain.Example;
using Doomain.Shared;

namespace Doomain.WebApiExample.Mappings
{
    public class ModelAProfile : Profile
    {
        public ModelAProfile(ICoder coder)
        {
            CreateMap<Modeladto, ModelA>().ConstructUsing(
                src => BuildModel(coder, src));

            CreateMap<ModelA, Modeladto>();
        }

        private static ModelA BuildModel(ICoder coder, Modeladto src)
        {
            var model = new ModelA(coder);
            model.SetName(src.Name);
            model.SetGuid(src.Id);
            model.Revision = src.Revision;
            return model;
        }
    }
}
