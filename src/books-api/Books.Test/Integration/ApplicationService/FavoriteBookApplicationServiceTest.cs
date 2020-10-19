using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Models;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.ApplicationService
{
    public class FavoriteBookApplicationServiceTest : TestIntegrationBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteBookRepository _favoriteBookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestScope _requestScope;
        private readonly IFavoriteBookApplicationService _favoriteBookApplicationService;

        User currentUser; 

        public FavoriteBookApplicationServiceTest() : base()
        {
            CreateScope();

            _userRepository = GetIntanceScope<IUserRepository>();
            _favoriteBookRepository = GetIntanceScope<IFavoriteBookRepository>();
            _unitOfWork = GetIntanceScope<IUnitOfWork>();
            _requestScope = GetIntanceScope<IRequestScope>();
            _favoriteBookApplicationService = GetIntanceScope<IFavoriteBookApplicationService>();

            CreateUser();

            _requestScope.SetUserId(currentUser.Id);
        }

        private void CreateUser()
        {
            currentUser = new UserBuilder().Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
        }

        [Fact]
        public async Task FavoriteBookApplicationService_GetById()
        {
            var favoriteBook = new FavoriteBookBuilder().WithBookId("b8Z5DwAAQBAJ").WithUser(currentUser).Builder();

            _favoriteBookRepository.Add(favoriteBook);
            _unitOfWork.Commit();

            var model = await _favoriteBookApplicationService.GetById(favoriteBook.Id);

            model.Should().NotBeNull();
        }

        [Fact]
        public void FavoriteBookApplicationService_Delete()
        {
            var favoriteBook = new FavoriteBookBuilder().WithBookId("b8Z5DwAAQBAJ").WithUser(currentUser).Builder();

            _favoriteBookRepository.Add(favoriteBook);
            _unitOfWork.Commit();

            _favoriteBookApplicationService.Delete(favoriteBook.Id);

            var result = _favoriteBookRepository.Get(new Filter());

            result.totalItems.Should().Be(0);
            result.entities.Should().HaveCount(0);
        }

        [Fact]
        public void FavoriteBookApplicationService_Add()
        {
            var model = new FavoriteBookModel
            {
                Title = "test",
                Description = "test",
                Authors = new List<string>() { "test", "test2" },
                BookId = "test",
                Link = "test",
                Thumbnail = "test"
            };

            _favoriteBookApplicationService.Add(model);

            var result = _favoriteBookRepository.Get(new Filter());

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
        }
    }
}
