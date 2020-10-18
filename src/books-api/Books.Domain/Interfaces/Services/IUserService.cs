using System;
using Books.Domain.DTO;

namespace Books.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void Add(UserDto dto);
        void Register(UserDto dto);
        void Update(Guid id, UserDto dto);
        void Delete(Guid id);
        void Enable(Guid id);
        void Disable(Guid id);
    }
}
