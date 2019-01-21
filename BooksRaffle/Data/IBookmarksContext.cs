using System;
using System.Data.Entity;
using BooksRaffle.Models;

namespace BooksRaffle.Data
{
    public interface IBookmarksContext : IDisposable
    {
        DbSet<Bookmark> Bookmarks { get; set; }

        DbSet<Tag> Tags { get; set; }

        int SaveChanges();
    }
}