using System;
using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface IRequestScope
    {
        void SetUserId(Guid id);
        User GetUser();
    }
}
