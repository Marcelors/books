using System.Linq;
using Books.Domain.Interfaces;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Nofication;
using MediatR;

namespace Books.Domain.Services
{
    public class Service
    {
        private readonly DomainNotificationHandler _notifications;
        protected readonly IUnitOfWork Uow;
        protected readonly IMediator Bus;

        protected Service(IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<DomainNotification> notifications)
        {
            Uow = uow;
            Bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationError(BaseDto dto)
        {
            if (dto == null)
            {
                return;
            }

            Bus.Publish(new DomainNotification(string.Join("\n ", dto.ValidationResult.Errors.Select(x => x.ErrorMessage))));
        }

        protected void NotifyError(string message)
        {
            Bus.Publish(new DomainNotification(message));
        }

        protected bool Commit()
        {

            if (_notifications.HasNotifications()) return false;

            if (Uow.Commit()) return true;

            Bus.Publish(new DomainNotification("Tivemos um problema ao salvar seus dados."));
            return false;
        }

    }
}
