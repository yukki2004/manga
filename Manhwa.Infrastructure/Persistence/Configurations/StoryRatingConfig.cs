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
    public class StoryRatingConfig : IEntityTypeConfiguration<StoryRating>
    {
        public void Configure(EntityTypeBuilder<StoryRating> e)
        {
            e.ToTable("story_ratings");

            e.HasKey(x => new { x.StoryId, x.UserId });
            e.HasOne(x => x.User)
                .WithMany(x => x.StoryRatings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Story)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
