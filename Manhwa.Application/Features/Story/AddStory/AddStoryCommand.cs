using Manhwa.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.AddStory
{
    public record CreateStoryCommand(CreateStoryRequest Request, int CurrentUserId, bool IsAdmin)
        : IRequest<CreateStoryResponse>;
}
