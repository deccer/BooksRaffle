using System;
using BooksRaffle.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksRaffle.Data
{
    public interface IBookmarksContext : IDisposable
    {
        DbSet<Bookmark> Bookmarks { get; set; }

        DbSet<Tag> Tags { get; set; }

        int SaveChanges();
    }
}