using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Application
{
    public class BookApplicationService : IBookApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BookApplicationService(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<PaginedModel<BookModel>> Get(Filter filter)
        {
            var (totalItems, books) = await _bookService.Get(filter);
            return new PaginedModel<BookModel>(totalItems, _mapper.Map<IList<BookModel>>(books));
        }

        public async Task<BookModel> GetById(string id)
        {
            var book = await _bookService.GetById(id);
            return _mapper.Map<BookModel>(book);
        }
    }
}
