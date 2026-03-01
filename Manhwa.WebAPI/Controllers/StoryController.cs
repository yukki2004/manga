using Manhwa.Application.Features.Story.AddStory;
using Manhwa.Application.Features.Story.DeleteStory;
using Manhwa.Application.Features.Story.UpdateStory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateStoryRequest request)
        {
            if (request.Thumbnail == null || request.Thumbnail.Length == 0)
                return BadRequest("Thumbnail is required");

            if (request.GenreIds == null || request.GenreIds.Count == 0)
                return BadRequest("At least one genre is required");

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var result = await _mediator.Send(
                new CreateStoryCommand(request, userId, true));

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteStoryCommand(id, true));
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateStoryRequest request)
        {
            var result = await _mediator.Send(
                new UpdateStoryCommand(id, request, true));

            return Ok(result);
        }
    }
}
