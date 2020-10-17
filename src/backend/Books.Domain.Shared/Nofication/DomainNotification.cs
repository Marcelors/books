using System;
using MediatR;

namespace Books.Domain.Shared.Nofication
{
    public class DomainNotification : INotification
    {
        public string Value { get; private set; }
        public Guid Id { get; private set; }


        public DomainNotification(string value)
        {
            Value = value;
            Id = Guid.NewGuid();
        }
    }
}
