using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.UpdateStory
{
    public record UpdateStoryCommand(
        int StoryId,
        UpdateStoryRequest Request,
        bool IsAdmin
    ) : IRequest<UpdateStoryResponse>;
}
