using System.Data.Entity;
using BooksRaffle.Models;

namespace BooksRaffle.Data
{
    public class BookmarksContext : DbContext, IBookmarksContext
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Bookmark> Bookmarks { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Tag> Tags { get; set; }

        public BookmarksContext()
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Database=Bookmarks;Trusted_Connection=True")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookmarkTag>().HasKey(tag => new { tag.BookmarkId, tag.TagId });
            modelBuilder.Entity<BookmarkTag>()
                .HasRequired(bt => bt.Bookmark)
                .WithMany(b => b.BookmarkTags)
                .HasForeignKey(bt => bt.BookmarkId);
            modelBuilder.Entity<BookmarkTag>()
                .HasRequired(bt => bt.Tag)
                .WithMany(b => b.BookmarkTags)
                .HasForeignKey(bt => bt.TagId);
        }
    }
}