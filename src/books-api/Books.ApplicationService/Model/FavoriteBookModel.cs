using System;
using System.Collections.Generic;

namespace Books.ApplicationService.Model
{
    public class FavoriteBookModel
    {
        public Guid Id { get; set; }
        public string BookId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public IList<string> Authors { get; set; } = new List<string>();
        public Guid? UserId { get; set; }
        public UserModel User { get; set; }
    }
}
