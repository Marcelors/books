using System.Collections.Generic;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Models;

namespace Books.Domain.Entities
{
    public class User : Entity
    {
        protected User()
        {

        }

        public User(string name, string password, string email, ProfileType profile)
        {
            Name = name;
            Password = password;
            Email = email;
            Active = true;
            Profile = profile;
        }

        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public bool Active { get; protected set; }
        public ProfileType Profile { get; protected set; }

        public IList<FavoriteBook> FavoriteBooks { get; set; } = new List<FavoriteBook>();

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        public void Enable()
        {
            Active = true;
        }

        public void Disable()
        {
            Active = false;
        }
    }
}
