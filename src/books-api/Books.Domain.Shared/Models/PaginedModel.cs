using System.Collections.Generic;

namespace Books.Domain.Shared.Models
{
    public class PaginedModel<T> where T : class
    {
        public PaginedModel(int totalItems, IList<T> items)
        {
            TotalItems = totalItems;
            Items = items;
        }

        public int TotalItems { get; set; }
        public IList<T> Items { get; set; }
    }
}
