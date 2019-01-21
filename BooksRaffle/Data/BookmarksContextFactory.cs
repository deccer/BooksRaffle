namespace BooksRaffle.Data
{
    public class BookmarksContextFactory : IBookmarksContextFactory
    {
        public IBookmarksContext CreateBookmarksContext()
        {
            return new BookmarksContext();
        }
    }
}