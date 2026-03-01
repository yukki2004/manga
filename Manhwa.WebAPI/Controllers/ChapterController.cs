using Manhwa.Application.Features.Chapter.CreateChapter;
using Manhwa.Application.Features.Chapter.DeleteChapter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(int storyId, [FromForm] CreateChapterRequest request)
        {
            if (request.Pages == null || request.Pages.Count == 0)
                return BadRequest("At least one page is required");

            var result = await _mediator.Send(
                new CreateChapterCommand(storyId, request, true));

            return Ok(result);
        }
        [HttpDelete("{storyId}/{chapterId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int storyId, int chapterId)
        {
            var isAdmin = User.IsInRole("Admin");

            await _mediator.Send(
                new DeleteChapterCommand(storyId, chapterId, isAdmin));

            return NoContent();
        }
    }
}

