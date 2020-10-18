using System.Collections.Generic;
using Books.Domain.DTO;
using Books.Domain.Shared.Models;

namespace Books.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public (int totalItems, IList<BookDto> books) Get(Filter filter);
        public BookDto GetById(string id);
    }
}
