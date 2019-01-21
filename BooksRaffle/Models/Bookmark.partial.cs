using System;

namespace BooksRaffle.Models
{
    public partial class Bookmark : IEquatable<Bookmark>
    {
        public string ShortCreatedDate => CreatedDate.ToString("yyyy-MM-dd");

        public bool Equals(Bookmark other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id
                   && string.Equals((string) Site, (string) other.Site)
                   && string.Equals((string) Url, (string) other.Url)
                   && CreatedDate.Equals(other.CreatedDate)
                   && Equals(BookmarkTags, other.BookmarkTags);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bookmark)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Site != null ? Site.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreatedDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (BookmarkTags != null ? BookmarkTags.GetHashCode() : 0);
                return hashCode;
            }
        }

    }
}