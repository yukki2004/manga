using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manhwa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/volume")]
    public class VolumeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VolumeController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
