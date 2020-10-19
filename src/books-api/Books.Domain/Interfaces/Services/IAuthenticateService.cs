using Books.Domain.DTO;
using Books.Domain.Entities;

namespace Books.Domain.Interfaces.Services
{
    public interface IAuthenticateService
    {
        (string token, User user) Authenticate(AuthenticateDto dto);
    }
}
