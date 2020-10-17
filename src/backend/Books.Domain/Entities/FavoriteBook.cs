using System;
using Books.Domain.Shared.Models;

namespace Books.Domain.Entities
{
    public class FavoriteBook : Entity
    {
        public FavoriteBook(string bookId, string link, string title, string thumbnail, string description, string authors, User user)
        {
            BookId = bookId;
            Link = link;
            Title = title;
            Thumbnail = thumbnail;
            Description = description;
            Authors = authors;
            UserId = user.Id;
            User = user;
        }

        protected FavoriteBook() { }


        public string BookId { get; protected set; }
        public string Link { get; protected set; }
        public string Title { get; protected set; }
        public string Thumbnail { get; protected set; }
        public string Description { get; protected set; }
        public string Authors { get; protected set; }
        public Guid UserId { get; protected set; }
        public User User { get; protected set; }
    }
}
