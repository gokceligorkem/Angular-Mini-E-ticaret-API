using EticaretAPI.Application.Abstraction.Services.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        IApplicationService _applicationService;

        public ServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }   
        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndpoint()
        {
           var datas= _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
