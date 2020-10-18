using System;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;

namespace Books.Domain.Authentication
{
    public class RequestScope : IRequestScope
    {
        private readonly IUserRepository _userRepository;
        private User user;

        public RequestScope(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser()
        {
            return user;
        }

        public void SetUserId(Guid id)
        {
            user = _userRepository.GetById(id);
        }
    }
}
