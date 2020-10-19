using System;
using Books.Domain.Entities;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Extensions;

namespace Books.Test.Builder
{
    public class UserBuilder
    {
        private string _name;
        private string _password;
        private string _email;
        private bool? _active;
        private ProfileType? _profile;

        public User Builder()
        {
            var user = new User(name: _name.HasValue() ? _name : "test",
                password: _password.HasValue() ? _password.Encrypt() : "test".Encrypt(),
                email: _email.HasValue() ? _email : "test@com.br",
                profile: _profile.HasValue ? _profile.Value : ProfileType.Administrator);

            if (_active.HasValue)
            {
                user.SetActive(_active.Value);
            }

            user.WithId();
            return user;
        }

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithActive(bool active)
        {
            _active = active;
            return this;
        }

        public UserBuilder WithProfile(ProfileType profile)
        {
            _profile = profile;
            return this;
        }
    }
}
