using Manhwa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Story> Stories => Set<Story>();
        public DbSet<Domain.Entities.Type> Types => Set<Domain.Entities.Type>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<StoryGenre> StoryGenres => Set<StoryGenre>();
        public DbSet<Chapter> Chapters => Set<Chapter>();
        public DbSet<ChapterImage> ChapterImages => Set<ChapterImage>();
        public DbSet<Volume> Volumes => Set<Volume>();
        public DbSet<VolumeImage> VolumeImages => Set<VolumeImage>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
        public DbSet<StoryBookmark> StoryBookmarks => Set<StoryBookmark>();
        public DbSet<StoryRating> StoryRatings => Set<StoryRating>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<ReadingHistory> ReadingHistories => Set<ReadingHistory>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
