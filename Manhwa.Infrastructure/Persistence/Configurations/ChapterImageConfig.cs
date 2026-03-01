using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class ChapterImageConfig : IEntityTypeConfiguration<ChapterImage>
    {
        public void Configure(EntityTypeBuilder<ChapterImage> e)
        {
            e.ToTable("chapter_images");

            e.HasKey(x => x.ChapterImageId);

            e.Property(x => x.ImageUrl)
                .IsRequired();

            e.Property(x => x.OrderIndex)
                .IsRequired();

            e.HasOne(x => x.Chapter)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}