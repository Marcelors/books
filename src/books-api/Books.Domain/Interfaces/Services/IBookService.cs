using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Domain.DTO;
using Books.Domain.Shared.Models;

namespace Books.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public Task<(int totalItems, IList<BookDto> books)> Get(Filter filter);
        public Task<BookDto> GetById(string id);
    }
}
