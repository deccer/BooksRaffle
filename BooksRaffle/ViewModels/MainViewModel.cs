using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookmarksRaffle.Commands;
using BookmarksRaffle.Data;
using BookmarksRaffle.Extensions;
using BooksRaffle.Models;
using Bookmark = BooksRaffle.Models.Bookmark;
using Tag = BooksRaffle.Models.Tag;

namespace BooksRaffle.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private string _filterText;
        private string _bookmarkUrl;
        private string _bookmarkTags;

        public ObservableCollection<Bookmark> Bookmarks { get; private set; }

        public ObservableCollection<Bookmark> FilteredBookmarks
        {
            get
            {
                if (string.IsNullOrEmpty(FilterText))
                {
                    return Bookmarks;
                }
                var results = new List<Bookmark>();
                var filters = FilterText.Split(' ');
                foreach (var filter in filters)
                {
                    var matchingBookmarks = Bookmarks.Where(bm =>
                        bm.Site.Contains(filter) || bm.Url.Contains(filter) ||
                        bm.BookmarkTags.Any(bmt => bmt.Tag != null && bmt.Tag.Name.Contains(filter)));
                    results.AddRange(matchingBookmarks);
                }
                return new ObservableCollection<Bookmark>(results);
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                SetValue(ref _filterText, value, () =>
                {
                    OnPropertyChanged(nameof(FilteredBookmarks));
                });
            }
        }

        public string BookmarkUrl
        {
            get => _bookmarkUrl;
            set => SetValue(ref _bookmarkUrl, value);
        }

        public string BookmarkTags
        {
            get => _bookmarkTags;
            set => SetValue(ref _bookmarkTags, value);
        }

        public ICommand AddBookmarkCommand { get; }

        public ICommand RemoveBookmarkCommand { get; }

        public MainViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                LoadBookmarks();
            }

            AddBookmarkCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrEmpty(BookmarkUrl))
                {
                    return;
                }

                using (var db = new BookmarksContext())
                {
                    var bookmark = db.Bookmarks.FirstOrDefault(bm => bm.Url == BookmarkUrl);
                    if (bookmark == null)
                    {
                        if (!Uri.TryCreate(BookmarkUrl, UriKind.RelativeOrAbsolute, out var uri))
                        {
                            return;
                        }

                        bookmark = new Bookmark
                        {
                            CreatedDate = DateTime.Now,
                            Site = uri.Host,
                            Url = uri.ToString()
                        };

                    }

                    if (!string.IsNullOrEmpty(BookmarkTags))
                    {
                        var tagNames = BookmarkTags.Split(' ');
                        if (tagNames.Length > 0)
                        {
                            var tags = db.Tags.Where(tag => tagNames.Contains(tag.Name)).ToList()
                                .Union(tagNames.Select(tagName => new Tag { Name = tagName })).DistinctBy(tag => tag.Name);
                            bookmark.BookmarkTags.Clear();
                            foreach (var tag in tags)
                            {
                                bookmark.BookmarkTags.Add(new BookmarkTag { Bookmark = bookmark, Tag = tag });
                            }
                        }
                    }

                    db.Bookmarks.Add(bookmark);
                    db.SaveChanges();

                    BookmarkUrl = string.Empty;
                    BookmarkTags = string.Empty;
                }
                LoadBookmarks();

            });
            RemoveBookmarkCommand = new DelegateCommand<Bookmark>(bookmark =>
            {
                using (var db = new BookmarksContext())
                {
                    var bm = db.Bookmarks.FirstOrDefault(b => b.Id == bookmark.Id);
                    if (bm != null)
                    {
                        db.Bookmarks.Remove(bm);
                    }
                    db.SaveChanges();
                }
                LoadBookmarks();
            });
        }

        private void LoadBookmarks()
        {
            using (var db = new BookmarksContext())
            {
                Bookmarks = new ObservableCollection<Bookmark>(db.Bookmarks.Include("BookmarkTags.Tag").ToList());
                OnPropertyChanged(nameof(FilteredBookmarks));
            }
        }
    }
}