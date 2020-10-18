using System;
using System.Linq;
using System.Threading.Tasks;
using Books.Domain.DTO;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Nofication;
using Books.Domain.Shared.Resources;
using MediatR;

namespace Books.Domain.Services
{
    public class FavoriteBookService : Service, IFavoriteBookService
    {
        private readonly IFavoriteBookRepository _favoriteBookRepository;
        private readonly IBookService _bookService;

        public FavoriteBookService(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications, IFavoriteBookRepository favoriteBookRepository, IBookService bookService) : base(uow, bus, notifications)
        {
            _favoriteBookRepository = favoriteBookRepository;
            _bookService = bookService;
        }

        public void Add(FavoriteBookDto dto)
        {
            if (!dto.IsValid())
            {
                NotifyValidationError(dto);
                return;
            }

            var authors = string.Join(";", dto.Authors.Select(x => x));
            var favoriteBook = new FavoriteBook(bookId: dto.BookId, link: dto.Link, title: dto.Title, thumbnail: dto.Thumbnail, description: dto.Description, authors: authors, user: dto.User);
            _favoriteBookRepository.Add(favoriteBook);
            Commit();
        }

        public void Delete(Guid id)
        {
            _favoriteBookRepository.Remove(id);
            Commit();
        }

        public async Task<BookDto> GetById(Guid id)
        {
            var favoriteBook = _favoriteBookRepository.GetById(id);

            if(favoriteBook == null)
            {
                NotifyError(DomainError.FavoriteBookNotFound);
                return null;
            }

            var book = await _bookService.GetById(favoriteBook.BookId);
            return book;
        }
    }
}
