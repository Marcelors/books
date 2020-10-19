using System;
using Books.Domain.DTO;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Nofication;
using Books.Domain.Shared.Resources;
using MediatR;

namespace Books.Domain.Services
{
    public class AuthenticateService : Service, IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenEncoder _tokenEncoder;

        public AuthenticateService(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications, IUserRepository userRepository, ITokenEncoder tokenEncoder) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _tokenEncoder = tokenEncoder;
        }

        public (string token, User user) Authenticate(AuthenticateDto dto)
        {
            if (!dto.IsValid())
            {
                NotifyValidationError(dto);
                return (string.Empty, null);
            }

            var user = _userRepository.GetByEmail(dto.Email);

            if(user == null)
            {
                NotifyError(DomainError.UserNotFound);
                     return (string.Empty, null);
            }

            if(user.Password != dto.Password.Encrypt())
            {
                NotifyError(DomainError.InvalidPassoword);
                     return (string.Empty, null);
            }

            var token = _tokenEncoder.Encoder(user);

            return (token, user);
        }
    }
}
