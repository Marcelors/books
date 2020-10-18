using System;
using System.Threading.Tasks;
using Books.Domain.DTO;

namespace Books.Domain.Interfaces.Services
{
    public interface IFavoriteBookService
    {
        void Add(FavoriteBookDto dto);
        void Delete(Guid id);
        Task<BookDto> GetById(Guid id);
    }
}
