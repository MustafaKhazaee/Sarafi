using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Sarafi.API.Controllers
{
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
