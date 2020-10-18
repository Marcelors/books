using Books.Domain.Shared.Enums;

namespace Books.Domain.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ProfileType? Profile { get; set; }
    }
}
