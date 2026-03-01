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
    public class VolumeConfig : IEntityTypeConfiguration<Volume>
    {
        public void Configure(EntityTypeBuilder<Volume> e)
        {
            e.ToTable("volumes");

            e.HasKey(x => x.VolumeId);

            e.HasOne(x => x.Story)
                .WithMany(x => x.Volumes)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Language)
                .WithMany(x=> x.Volumes)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Images)
                .WithOne(x => x.Volume)
                .HasForeignKey(x => x.VolumeId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Notifications)
                .WithOne(x => x.Volume)
                .HasForeignKey(x => x.VolumeId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.ReadingHistories)
                .WithOne(x => x.Volume)
                .HasForeignKey(x => x.VolumeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
