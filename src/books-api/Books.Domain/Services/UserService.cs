using System;
using Books.Domain.DTO;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Nofication;
using Books.Domain.Shared.Resources;
using MediatR;

namespace Books.Domain.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestScope _requestScope;

        public UserService(IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository,
            IRequestScope requestScope) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _requestScope = requestScope;
        }

        public void Add(UserDto dto)
        {
            if (!dto.IsValid())
            {
                NotifyValidationError(dto);
                return;
            }

            var currentUser = _requestScope.GetUser();

            if(currentUser.Profile == ProfileType.Standard)
            {
                NotifyError(DomainError.StandardProfileUserDoesNotHavePermission);
                return;
            }

            var user = new User(dto.Name, dto.Password, dto.Email, dto.Profile.Value);
            _userRepository.Add(user);
            Commit();
        }

        public void Delete(Guid id)
        {
            var currentUser = _requestScope.GetUser();

            if (currentUser.Profile == ProfileType.Standard)
            {
                NotifyError(DomainError.StandardProfileUserDoesNotHavePermission);
                return;
            }

            _userRepository.Remove(id);
            Commit();
        }

        public void Disable(Guid id)
        {
            var currentUser = _requestScope.GetUser();

            if (currentUser.Profile == ProfileType.Standard)
            {
                NotifyError(DomainError.StandardProfileUserDoesNotHavePermission);
                return;
            }

            var user = _userRepository.GetById(id);

            if(user == null)
            {
                NotifyError(DomainError.UserNotFound);
                return;
            }

            user.Disable();
            _userRepository.Update(user);
            Commit();
        }

        public void Enable(Guid id)
        {
            var currentUser = _requestScope.GetUser();

            if (currentUser.Profile == ProfileType.Standard)
            {
                NotifyError(DomainError.StandardProfileUserDoesNotHavePermission);
                return;
            }

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                NotifyError(DomainError.UserNotFound);
                return;
            }

            user.Enable();
            _userRepository.Update(user);
            Commit();
        }

        public void Register(UserDto dto)
        {
            if (!dto.IsValid())
            {
                NotifyValidationError(dto);
                return;
            }

            var user = new User(dto.Name, dto.Password, dto.Email, dto.Profile.Value);
            _userRepository.Add(user);
            Commit();
        }

        public void Update(Guid id, UserDto dto)
        {
            var currentUser = _requestScope.GetUser();

            if(currentUser.Profile == ProfileType.Standard && currentUser.Id != id)
            {
                NotifyError(DomainError.StandardProfileUserDoesNotHavePermission);
                return;
            }

            if (!dto.IsValid())
            {
                NotifyValidationError(dto);
                return;
            }

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                NotifyError(DomainError.UserNotFound);
                return;
            }

            user.SetName(dto.Name);
            user.SetActive(dto.Active);
            user.SetProfile(dto.Profile.Value);

            _userRepository.Update(user);
            Commit();
        }
    }
}
