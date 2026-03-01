using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Chapter.CreateChapter
{
    public record CreateChapterCommand(
        int StoryId,
        CreateChapterRequest Request,
        bool IsAdmin
    ) : IRequest<CreateChapterResponse>;
}
