using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WooliesChallengeApi.Controllers
{
    [Route("api/answers")]
    [ApiController]

    public class BaseController : ControllerBase {
        private IMediator _mediator;
        
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
