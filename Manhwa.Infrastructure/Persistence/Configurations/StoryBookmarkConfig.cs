using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class StoryBookmarkConfig : IEntityTypeConfiguration<StoryBookmark>
    {
        public void Configure(EntityTypeBuilder<StoryBookmark> e)
        {
            e.ToTable("story_bookmarks");

            e.HasKey(x => new { x.StoryId, x.UserId, x.BookmarkId });

            e.HasOne(x => x.User)
                .WithMany(x => x.StoryBookmarks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Story)
                .WithMany(x => x.StoryBookmarks)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Bookmark)
                .WithMany(x => x.StoryBookmarks)
                .HasForeignKey(x => x.BookmarkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}