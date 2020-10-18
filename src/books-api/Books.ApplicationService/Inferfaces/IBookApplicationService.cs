using System;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Inferfaces
{
    public interface IBookApplicationService
    {
        BookModel GetById(Guid id);
        PaginedModel<BookModel> Get(Filter filter);
    }
}
