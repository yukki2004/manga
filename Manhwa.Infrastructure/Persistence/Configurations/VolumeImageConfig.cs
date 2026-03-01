using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class VolumeImageConfig : IEntityTypeConfiguration<VolumeImage>
    {
        public void Configure(EntityTypeBuilder<VolumeImage> e)
        {
            e.ToTable("volume_images");

            e.HasKey(x => x.VolumeImageId);

            e.Property(x => x.ImageUrl)
                .IsRequired();

            e.Property(x => x.OrderIndex)
                .IsRequired();

            e.HasOne(x => x.Volume)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.VolumeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}