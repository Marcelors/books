using System;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short? Profile { get; set; }
        public EnumModel<short> ProfileModel { get; set; }
    }
}
