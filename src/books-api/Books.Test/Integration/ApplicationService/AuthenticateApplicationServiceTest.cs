using System;
using System.Linq;
using Books.ApplicationService.Application;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Resources;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.ApplicationService
{
    public class AuthenticateApplicationServiceTest : TestIntegrationBase
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        private IAuthenticateApplicationService _authenticateApplicationService;

        public AuthenticateApplicationServiceTest() : base()
        {
            CreateScope();

            _userRepository = GetIntanceScope<IUserRepository>();
            _unitOfWork = GetIntanceScope<IUnitOfWork>();
            _authenticateApplicationService = GetIntanceScope<IAuthenticateApplicationService>();

        }

        [Fact]
        public void AuthenticateApplicationService_Authenticate()
        {
            var user = new UserBuilder().WithEmail("test@mail.com").WithPassword("qwe123").Builder();

            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new AuthenticateModel
            {
                Email = user.Email,
                Password = "qwe123"
            };

            var result = _authenticateApplicationService.Authenticate(model);

            result.Token.Should().NotBeEmpty();
            result.User.Should().NotBeNull();
        }

        [Fact]
        public void AuthenticateApplicationService_Authenticate_user_not_found()
        {
            var user = new UserBuilder().WithEmail("test@mail.com").WithPassword("qwe123").Builder();

            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new AuthenticateModel
            {
                Email = "mail@mail.com",
                Password = "qwe123"
            };

            var result = _authenticateApplicationService.Authenticate(model);

            result.Token.Should().BeNullOrEmpty();
            result.User.Should().BeNull();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.UserNotFound);
        }

        [Fact]
        public void AuthenticateApplicationService_Authenticate_invalid_password()
        {
            var user = new UserBuilder().WithEmail("test@mail.com").WithPassword("qwe123").Builder();

            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new AuthenticateModel
            {
                Email = user.Email,
                Password = "qwe123456"
            };

            var result = _authenticateApplicationService.Authenticate(model);

            result.Token.Should().BeNullOrEmpty();
            result.User.Should().BeNull();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.InvalidPassoword);
        }
    }
}
