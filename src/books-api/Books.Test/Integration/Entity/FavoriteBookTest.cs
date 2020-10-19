using System;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.Entity
{
    public class FavoriteBookTest : TestIntegrationBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteBookRepository _favoriteBookRepository;

        public FavoriteBookTest() : base()
        {
            _userRepository = GetInstance<IUserRepository>();
            _unitOfWork = GetInstance<IUnitOfWork>();
            _favoriteBookRepository = GetInstance<IFavoriteBookRepository>();
        }

        [Fact]
        public void Favorite_test()
        {
            var user = new UserBuilder().Builder();
            var favoriteBook = new FavoriteBookBuilder().WithUser(user).Builder();

            _userRepository.Add(user);
            _favoriteBookRepository.Add(favoriteBook);
            _unitOfWork.Commit();

            var result = _favoriteBookRepository.GetById(favoriteBook.Id);

            result.Should().NotBeNull();
            result.Should().Be(favoriteBook);
        }
    }
}
