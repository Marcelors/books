using System;
using System.Linq;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Resources;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.ApplicationService
{
    public class UserApplicationServiceTest : TestIntegrationBase
    {
        private readonly IUserApplicationService _userApplicationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestScope _requestScope;

        public UserApplicationServiceTest() : base()
        {
            CreateScope();

            _userRepository = GetIntanceScope<IUserRepository>();
            _unitOfWork = GetIntanceScope<IUnitOfWork>();
            _requestScope = GetIntanceScope<IRequestScope>();
            _userApplicationService = GetIntanceScope<IUserApplicationService>();
        }

        [Fact]
        public void UserApplicationService_Register()
        {
            var model = new UserModel
            {
                Name = "test",
                Email = "test@mail.com",
                Password = "test"
            };

            _userApplicationService.Register(model);

            var result = _userRepository.Get(new Filter());

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
        }

        [Fact]
        public void UserApplicationService_Add_without_permission()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Standard).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var model = new UserModel
            {
                Name = "test",
                Email = "test@mail.com",
                Password = "test",
                Profile = (short)ProfileType.Standard
            };

            _userApplicationService.Add(model);

            var result = _userRepository.Get(new Filter());

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.StandardProfileUserDoesNotHavePermission);
        }

        [Fact]
        public void UserApplicationService_Add()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var model = new UserModel
            {
                Name = "test",
                Email = "test@mail.com",
                Password = "test",
                Profile = (short)ProfileType.Standard
            };

            _userApplicationService.Add(model);

            var result = _userRepository.Get(new Filter());

            result.totalItems.Should().Be(2);
            result.entities.Should().HaveCount(2);
        }

        [Fact]
        public void UserApplicationService_Delete_without_permission()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Standard).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            var result = _userRepository.Get(new Filter());

            _userApplicationService.Delete(user.Id);

            result.totalItems.Should().Be(2);
            result.entities.Should().HaveCount(2);
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.StandardProfileUserDoesNotHavePermission);
        }

        [Fact]
        public void UserApplicationService_Delete()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Delete(user.Id);

            var result = _userRepository.Get(new Filter());

            result.totalItems.Should().Be(1);
            result.entities.Should().HaveCount(1);
        }

        [Fact]
        public void UserApplicationService_Enable_without_permission()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Standard).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(false).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Enable(user.Id);

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeFalse();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.StandardProfileUserDoesNotHavePermission);
        }

        [Fact]
        public void UserApplicationService_Enable()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(false).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Enable(user.Id);

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeTrue();
        }

        [Fact]
        public void UserApplicationService_Enable_not_found()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(false).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Enable(Guid.NewGuid());

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeFalse();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.UserNotFound);
        }

        [Fact]
        public void UserApplicationService_Disable_without_permission()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Standard).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Disable(user.Id);

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeTrue();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.StandardProfileUserDoesNotHavePermission);
        }

        [Fact]
        public void UserApplicationService_Disable()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Disable(user.Id);

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeFalse();
        }

        [Fact]
        public void UserApplicationService_Disable_not_found()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            _userApplicationService.Disable(Guid.NewGuid());

            var result = _userRepository.GetById(user.Id);

            result.Active.Should().BeTrue();
            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.UserNotFound);
        }

        [Fact]
        public void UserApplicationService_Update_without_permission()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Standard).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new UserModel
            {
                Name = "carpano",
                Email = user.Email,
                Password = user.Password,
                Profile = (short)user.Profile
            };

            _userApplicationService.Update(Guid.NewGuid(), model);

            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.StandardProfileUserDoesNotHavePermission);
        }

        [Fact]
        public void UserApplicationService_Update()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new UserModel
            {
                Name = "carpano",
                Email = user.Email,
                Password = user.Password,
                Profile = (short)user.Profile
            };

            _userApplicationService.Update(user.Id, model);

            var result = _userRepository.GetById(user.Id);

            result.Name.Should().Be(model.Name);
        }

        [Fact]
        public void UserApplicationService_Update_not_found()
        {
            var currentUser = new UserBuilder().WithProfile(ProfileType.Administrator).Builder();

            _userRepository.Add(currentUser);
            _unitOfWork.Commit();
            _requestScope.SetUserId(currentUser.Id);

            var user = new UserBuilder().WithProfile(ProfileType.Standard).WithActive(true).Builder();
            _userRepository.Add(user);
            _unitOfWork.Commit();

            var model = new UserModel
            {
                Name = "carpano",
                Email = user.Email,
                Password = user.Password,
                Profile = (short)user.Profile
            };

            _userApplicationService.Update(Guid.NewGuid(), model);

            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.UserNotFound);
        }
    }
}
