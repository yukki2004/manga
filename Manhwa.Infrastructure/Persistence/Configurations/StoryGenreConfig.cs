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
    public class StoryGenreConfig : IEntityTypeConfiguration<StoryGenre>
    {
        public void Configure(EntityTypeBuilder<StoryGenre> e)
        {
            e.ToTable("story_genres");

            e.HasKey(x => new { x.StoryId, x.GenreId });

            e.HasOne(x => x.Story)
                .WithMany(x => x.StoryGenres)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Genre)
                .WithMany(x => x.StoryGenres)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
