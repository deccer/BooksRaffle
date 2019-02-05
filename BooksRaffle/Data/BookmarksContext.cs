using BooksRaffle.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksRaffle.Data
{
    public class BookmarksContext : DbContext, IBookmarksContext
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Bookmark> Bookmarks { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Tag> Tags { get; set; }

        public BookmarksContext()
            : base(new DbContextOptions<DbContext>())
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlite("Data Source=Books.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookmarkTag>().HasKey(tag => new { tag.BookmarkId, tag.TagId });
            modelBuilder.Entity<BookmarkTag>()
                .HasOne(bt => bt.Bookmark)
                .WithMany(b => b.BookmarkTags)
                .HasForeignKey(bt => bt.BookmarkId);
            modelBuilder.Entity<BookmarkTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(b => b.BookmarkTags)
                .HasForeignKey(bt => bt.TagId);
        }
    }
}