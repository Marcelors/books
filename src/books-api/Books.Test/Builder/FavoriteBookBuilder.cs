using Books.Domain.Entities;
using Books.Domain.Shared.Extensions;

namespace Books.Test.Builder
{
    public class FavoriteBookBuilder
    {
        private string _bookId;
        private string _link;
        private string _title;
        private string _thumbnail;
        private string _description;
        private string _authors;
        private User _user;

        public FavoriteBook Builder()
        {
            var favoriteBook = new FavoriteBook(bookId: _bookId.HasValue() ? _bookId : "test",
                link: _link.HasValue() ? _link : "test",
                title: _title.HasValue() ? _title : "test",
                thumbnail: _thumbnail.HasValue() ? _thumbnail : "test",
                description: _description.HasValue() ? _description : "dest",
                authors: _authors.HasValue() ? _authors : "test",
                user: _user != null ? _user : new UserBuilder().Builder());

            favoriteBook.WithId();

            return favoriteBook;
        }

        public FavoriteBookBuilder WithBookId(string bookId)
        {
            _bookId = bookId;
            return this;
        }

        public FavoriteBookBuilder WithLink(string link)
        {
            _link = link;
            return this;
        }

        public FavoriteBookBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public FavoriteBookBuilder WithThumbnail(string thumbnail)
        {
            _thumbnail = thumbnail;
            return this;
        }

        public FavoriteBookBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public FavoriteBookBuilder WithAuthors(string authors)
        {
            _authors = authors;
            return this;
        }

        public FavoriteBookBuilder WithUser(User user)
        {
            _user = user;
            return this;
        }

    }
}
