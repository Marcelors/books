using System;
using System.Linq;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Models;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.Repository
{
    public class FavoriteBookRepositoryTest : TestIntegrationBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IFavoriteBookRepository _favoriteBookRepository;

        public FavoriteBookRepositoryTest() : base()
        {
            _userRepository = GetInstance<IUserRepository>();
            _unityOfWork = GetInstance<IUnitOfWork>();
            _favoriteBookRepository = GetInstance<IFavoriteBookRepository>();
        }

        [Fact]
        public void FavoriteBookRepository_Get()
        {
            var user_1 = new UserBuilder().WithName("Pedro").Builder();
            var favoriteBook_1 = new FavoriteBookBuilder().WithUser(user_1).WithTitle("Vinho").Builder();
            var favoriteBook_2 = new FavoriteBookBuilder().WithUser(user_1).WithTitle("Carro").Builder();
            var favoriteBook_3 = new FavoriteBookBuilder().WithUser(user_1).WithTitle("Roupa").Builder();


            _userRepository.Add(user_1);
            _favoriteBookRepository.Add(favoriteBook_1);
            _favoriteBookRepository.Add(favoriteBook_2);
            _favoriteBookRepository.Add(favoriteBook_3);
            _unityOfWork.Commit();

            var filter = new Filter();

            var result = _favoriteBookRepository.Get(filter);

            result.totalItems.Should().Be(3);
            result.entities.Should().HaveCount(3);

            filter.Search = "car";
            result = _favoriteBookRepository.Get(filter);

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
            result.entities.First().Should().Be(favoriteBook_2);

            filter.Search = string.Empty;
            filter.CurrentPage = 1;
            filter.ItemsPerPage = 2;

            result = _favoriteBookRepository.Get(filter);

            result.totalItems.Should().Be(3);
            result.entities.Should().HaveCount(2);
        }
    }
}
