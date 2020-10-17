using System;
using Books.Domain.Interfaces;
using Books.Domain.Shared.Nofication;
using Books.Infra.Data.Context;
using MediatR;

namespace Books.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _context;
        private readonly IMediator _bus;

        public UnitOfWork(BookContext context, IMediator bus)
        {
            _context = context;
            _bus = bus;
        }

        public bool Commit()
        {
            try
            {
                return _context.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _bus.Publish(new DomainNotification(ex.Message));
            }
            return false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
