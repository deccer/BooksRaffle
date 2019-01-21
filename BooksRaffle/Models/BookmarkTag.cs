namespace BooksRaffle.Models
{
    public class BookmarkTag
    {
        public int BookmarkId { get; set; }

        public Bookmark Bookmark { get; set; }

        public int TagId { get; set; }

        public BooksRaffle.Models.Tag Tag { get; set; }
    }
}