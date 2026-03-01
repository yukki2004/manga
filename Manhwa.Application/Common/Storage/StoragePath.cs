using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Common.Storage
{
    public static class StoragePath
    {
        public static string StoryThumbnail(int storyId, string ext)
            => $"stories/{storyId}/thumbnail/cover{ext}";

        public static string VolumeThumbnail(int storyId, int volumeId, string ext)
            => $"stories/{storyId}/volumes/{volumeId}/thumbnail{ext}";

        public static string VolumePage(int storyId, int volumeId, int order, string ext)
            => $"stories/{storyId}/volumes/{volumeId}/pages/{order:D3}{ext}";

        public static string ChapterPage(int storyId, int chapterId, int order, string ext)
            => $"stories/{storyId}/chapters/{chapterId}/{order:D3}{ext}";
    }
}
