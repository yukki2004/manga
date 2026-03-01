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
    public class ReadingHistoryConfig : IEntityTypeConfiguration<ReadingHistory>
    {
        public void Configure(EntityTypeBuilder<ReadingHistory> builder)
        {
            builder.ToTable("reading_histories");

            // ✅ composite primary key
            builder.HasKey(x => new { x.UserId, x.StoryId });

            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.StoryId).HasColumnName("story_id");
            builder.Property(x => x.ChapterId).HasColumnName("chapter_id");
            builder.Property(x => x.VolumeId).HasColumnName("volume_id");
            builder.Property(x => x.LanguageId).HasColumnName("language_id");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            builder.HasOne(x => x.User)
                   .WithMany(x => x.ReadingHistories)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Story)
                   .WithMany(x => x.ReadingHistories)
                   .HasForeignKey(x => x.StoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Chapter)
                   .WithMany(x => x.ReadingHistories)
                   .HasForeignKey(x => x.ChapterId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Volume)
                   .WithMany(x => x.ReadingHistories)
                   .HasForeignKey(x => x.VolumeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Language)
                   .WithMany(x => x.ReadingHistories)
                   .HasForeignKey(x => x.LanguageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
