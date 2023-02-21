using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Sarafi.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SarafiV1Controller : ControllerBase
    {
        protected readonly IMediator mediator;
        public SarafiV1Controller (IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
