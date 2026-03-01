using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> e)
        {
            e.ToTable("genres");

            e.HasKey(x => x.GenreId);

            e.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(100);

            e.HasIndex(x => x.Slug).IsUnique();

            e.HasMany(x => x.StoryGenres)
                .WithOne(x => x.Genre)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}