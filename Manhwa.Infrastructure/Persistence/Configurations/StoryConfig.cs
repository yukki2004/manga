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
    public class StoryConfig : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> e)
        {
            e.ToTable("stories");

            e.HasKey(x => x.StoryId);

            e.HasOne(x => x.Type)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            e.HasMany(x => x.Chapters)
                .WithOne(x => x.Story)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Volumes)
                .WithOne(x => x.Story)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Ratings)
                .WithOne(x => x.Story)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.StoryBookmarks)
                .WithOne(x => x.Story)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.User)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            e.HasMany(x => x.ReadingHistories)
                .WithOne(x => x.Story)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
