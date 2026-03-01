using Manhwa.Application.Features.Volume.CreateVolume;
using Manhwa.Application.Features.Volume.DeleteVolume;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("{storyId}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
    [FromRoute]  int storyId,
    [FromForm] CreateVolumeRequest request)
        {
            if (request.Thumbnail == null || request.Thumbnail.Length == 0)
                return BadRequest("Thumbnail is required");

            if (request.Pages == null || request.Pages.Count == 0)
                return BadRequest("Pages are required");

            var isAdmin = User.IsInRole("Admin");

            var result = await _mediator.Send(
                new CreateVolumeCommand(storyId, isAdmin, request));

            return Ok(result);
        }
        [HttpDelete("{storyId}/{volumeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int storyId, [FromRoute] int volumeId)
        {
            var isAdmin = User.IsInRole("Admin");

            await _mediator.Send(
                new DeleteVolumeCommand(volumeId, isAdmin));

            return NoContent();
        }
    }
}
