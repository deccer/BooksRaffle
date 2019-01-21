using System;
using System.Collections.Generic;

namespace BooksRaffle.Models
{
    public partial class Tag : IEquatable<BooksRaffle.Models.Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BookmarkTag> BookmarkTags { get; set; } = new List<BookmarkTag>();
    }
}