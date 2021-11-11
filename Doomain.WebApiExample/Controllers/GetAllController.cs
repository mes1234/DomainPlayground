using AutoMapper;
using Doomain.Abstraction;
using Doomain.Example;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Doomain.WebApiExample.Controllers 
{ 
    /// <summary>
    /// 
    /// </summary>
[ApiController]
    public class DefaultApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ModelA> _repository;

        public DefaultApiController(
            IMapper mapper,
            IRepository<ModelA> repository)
        {
           _mapper = mapper;
            _repository = repository;
        }


        /// <summary>
        /// Returns a model A.
        /// </summary>
        /// <remarks>Returns a model A.</remarks>
        /// <param name="userId">id if model</param>
        /// <response code="200">A JSON array of models</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("//modela/{userId}")]
        public virtual IActionResult ModelaUserIdGet([FromRoute][Required] Guid userId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(ModelA));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(500);

            try
            {
                var model = _repository.Get(userId);

                var dto = _mapper.Map<Modeladto>(model);

                var serialized = JsonConvert.SerializeObject(dto);
                
                return StatusCode(200, dto);
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Add or update model A
        /// </summary>
        /// <param name="body">User credentials</param>
        /// <param name="userId">id if model</param>
        /// <response code="200">OK</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("//modela/{userId}")]
        public virtual IActionResult ModelaUserIdPost([FromBody] Modeladto body, [FromRoute][Required] Guid userId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(500);

            try
            {
                var model = _mapper.Map<ModelA>(body);
                _repository.AddOrUpdate(model);
                return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
