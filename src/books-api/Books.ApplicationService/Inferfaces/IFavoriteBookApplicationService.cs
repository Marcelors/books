using System;
using Books.ApplicationService.Model;
using Books.Domain.Entities;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Inferfaces
{
    public interface IFavoriteBookApplicationService
    {
        void Add(FavoriteBook model);
        void Delete(Guid id);
        BookModel GetById(Guid id);
        PaginedModel<FavoriteBook> Get(Filter filter);
    }
}
