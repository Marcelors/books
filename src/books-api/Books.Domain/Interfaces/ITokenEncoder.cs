using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface ITokenEncoder
    {
        string Encoder(User user);
    }
}
