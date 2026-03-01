using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class LanguageConfig : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> e)
        {
            e.ToTable("languages");

            e.HasKey(x => x.LanguageId);

            e.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            e.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            e.HasMany(x => x.Chapters)
                .WithOne(x => x.Language)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Volumes)
                .WithOne(x => x.Language)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.ReadingHistories)
                    .WithOne(x => x.Language)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}