using System.Threading.Tasks;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Inferfaces
{
    public interface IBookApplicationService
    {
        Task<BookModel> GetById(string id);
        Task<PaginedModel<BookModel>> Get(Filter filter);
    }
}
