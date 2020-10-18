using System;
using System.Threading.Tasks;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Inferfaces
{
    public interface IFavoriteBookApplicationService
    {
        void Add(FavoriteBookModel model);
        void Delete(Guid id);
        Task<BookModel> GetById(Guid id);
        PaginedModel<FavoriteBookModel> Get(Filter filter);
    }
}
