using Books.Domain.Entities;

namespace Books.Domain.Interfaces.Repositores
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByEmail(string email);
    }
}
