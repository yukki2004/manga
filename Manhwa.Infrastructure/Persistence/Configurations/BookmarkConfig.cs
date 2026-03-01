using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class BookmarkConfig : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> e)
        {
            e.ToTable("bookmarks");

            e.HasKey(x => x.BookmarkId);

            e.HasOne(x => x.User)
                .WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.StoryBookmarks)
                .WithOne(x => x.Bookmark)
                .HasForeignKey(x => x.BookmarkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
