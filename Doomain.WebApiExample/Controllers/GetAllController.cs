using Doomain.Abstraction;
using Doomain.Example;
using Microsoft.AspNetCore.Mvc;

namespace Doomain.WebApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllController : ControllerBase
    {
        private readonly IRepository<ModelA> _repository;

        public GetAllController(IRepository<ModelA> repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetInfo")]
        public string Get()
        {
           var m1 = _repository.Get(Guid.Parse("0721c89a-1437-4906-af53-da4d3880da6f"));
            return m1.Name;
        }
    }
}