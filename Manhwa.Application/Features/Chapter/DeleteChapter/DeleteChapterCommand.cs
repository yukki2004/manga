using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Chapter.DeleteChapter
{
    public record DeleteChapterCommand(
        int StoryId,
        int ChapterId,
        bool IsAdmin
    ) : IRequest;
}
