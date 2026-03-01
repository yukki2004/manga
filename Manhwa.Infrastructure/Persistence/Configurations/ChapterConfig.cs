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
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> e)
        {
            e.ToTable("chapters");

            e.HasKey(x => x.ChapterId);

            e.HasOne(x => x.Story)
                .WithMany(x => x.Chapters)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Language)
                .WithMany(x => x.Chapters)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Images)
                .WithOne(x => x.Chapter)
                .HasForeignKey(x => x.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Notifications)
                .WithOne(x => x.Chapter)
                .HasForeignKey(x => x.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.ReadingHistories)
                .WithOne(x => x.Chapter)
                .HasForeignKey(x => x.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
