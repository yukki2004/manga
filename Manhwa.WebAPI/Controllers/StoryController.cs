using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manhwa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/story")]
    public class StoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
