using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manhwa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/chapter")]
    public class ChapterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChapterController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
