using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRaffle.Models
{
    public partial class Bookmark
    {
        public int Id { get; set; }

        public string Site { get; set; }

        public string Url { get; set; }

        [DataType("datetime2")]
        public DateTime CreatedDate { get; set; }

        public List<BookmarkTag> BookmarkTags { get; set; } = new List<BookmarkTag>();
    }
}