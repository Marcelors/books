using System;
using Books.Domain.DTO;

namespace Books.Domain.Interfaces.Services
{
    public interface IFavoriteBookService
    {
        void Add(FavoriteBookDto dto);
        void Delete(Guid id);
        BookDto GetById(Guid id);
    }
}
