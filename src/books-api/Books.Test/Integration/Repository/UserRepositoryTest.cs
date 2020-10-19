using System.Linq;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Models;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.Repository
{
    public class UserRepositoryTest : TestIntegrationBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unityOfWork;

        public UserRepositoryTest() : base()
        {
            _userRepository = GetInstance<IUserRepository>();
            _unityOfWork = GetInstance<IUnitOfWork>();
        }

        [Fact]
        public void UserRepository_Get()
        {
            var user_1 = new UserBuilder().WithName("Pedro").Builder();
            var user_2 = new UserBuilder().WithName("Joao").Builder();
            var user_3 = new UserBuilder().WithName("Carlos").Builder();

            _userRepository.Add(user_1);
            _userRepository.Add(user_2);
            _userRepository.Add(user_3);
            _unityOfWork.Commit();

            var filter = new Filter();

            var result = _userRepository.Get(filter);

            result.totalItems.Should().Be(3);
            result.entities.Should().HaveCount(3);

            filter.Search = "car";
            result = _userRepository.Get(filter);

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
            result.entities.First().Should().Be(user_3);

            filter.Search = string.Empty;
            filter.CurrentPage = 1;
            filter.ItemsPerPage = 2;

            result = _userRepository.Get(filter);

            result.totalItems.Should().Be(3);
            result.entities.Should().HaveCount(2);
        }
    }
}
