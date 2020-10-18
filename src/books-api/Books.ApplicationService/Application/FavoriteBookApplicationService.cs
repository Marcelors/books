using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.DTO;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Application
{
    public class FavoriteBookApplicationService : IFavoriteBookApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteBookService _favoriteBookService;
        private readonly IFavoriteBookRepository _favoriteBookRepository;
        private readonly IRequestScope _requestScope;

        public FavoriteBookApplicationService(IMapper mapper, IFavoriteBookService favoriteBookService, IFavoriteBookRepository favoriteBookRepository, IRequestScope requestScope)
        {
            _mapper = mapper;
            _favoriteBookService = favoriteBookService;
            _favoriteBookRepository = favoriteBookRepository;
            _requestScope = requestScope;
        }

        public void Add(FavoriteBookModel model)
        {
            var dto = _mapper.Map<FavoriteBookDto>(model);
            dto.User = _requestScope.GetUser();
            _favoriteBookService.Add(dto);
        }

        public void Delete(Guid id)
        {
            _favoriteBookService.Delete(id);
        }

        public PaginedModel<FavoriteBookModel> Get(Filter filter)
        {
            var (totalItems, entities) = _favoriteBookRepository.Get(filter);
            return new PaginedModel<FavoriteBookModel>(totalItems, _mapper.Map<IList<FavoriteBookModel>>(entities));
        }

        public async Task<BookModel> GetById(Guid id)
        {
            var book = await _favoriteBookService.GetById(id);
            return _mapper.Map<BookModel>(book);
        }
    }
}
