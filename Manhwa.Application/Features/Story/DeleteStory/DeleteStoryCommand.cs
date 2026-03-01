using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.DeleteStory
{
    public record DeleteStoryCommand(int StoryId, bool IsAdmin) : IRequest;
}
