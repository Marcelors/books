using System;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.Entity
{
    public class UserTest : TestIntegrationBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserTest() : base()
        {
            _userRepository = GetInstance<IUserRepository>();
            _unitOfWork = GetInstance<IUnitOfWork>();
        }

        [Fact]
        public void User_test()
        {
            var user = new UserBuilder().Builder();

            _userRepository.Add(user);
            _unitOfWork.Commit();

            var result = _userRepository.GetById(user.Id);

            result.Should().NotBeNull();
            result.Should().Be(user);
        }
    }
}
