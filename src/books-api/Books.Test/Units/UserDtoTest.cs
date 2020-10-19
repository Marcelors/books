using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Books.Domain.DTO;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Parameters;
using Books.Domain.Shared.Resources;
using FluentAssertions;
using Xunit;

namespace Books.Test.Units
{
    public class UserDtoTest
    {
        [Theory]
        [ClassData(typeof(UserDataTest))]
        public void UserDto_is_not_valid(UserDto dto, string message)
        {
            var result = dto.IsValid();

            result.Should().BeFalse();
            dto.ValidationResult.Errors.Should().NotBeNullOrEmpty();
            dto.ValidationResult.Errors.First().ErrorMessage.Should().Be(message);
        }

        [Fact]
        public void UserDto_is_valid()
        {
            var dto = new UserDto
            {
                Name = "test",
                Email = "test@mail.com",
                Password = "test123",
                Profile = ProfileType.Administrator
            };

            var result = dto.IsValid();

            result.Should().BeTrue();

            dto.ValidationResult.Errors.Should().BeNullOrEmpty();
        }
    }

    public class UserDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Name
            yield return new object[]
            {
                new UserDto
                {
                    Name = null,
                    Email = "test@mail.com",
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                DomainError.NameIsRequired
            };
            yield return new object[]
            {
                new UserDto
                {
                    Name = string.Empty,
                    Email = "test@mail.com",
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                DomainError.NameIsRequired
            };
            yield return new object[]
            {
                new UserDto
                {
                    Name = StringExtension.GenerateString(DomainParameters.MaxLenghtOfTwoHundred + 1),
                    Email = "test@mail.com",
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                string.Format(DomainError.MaximumNameSize, DomainParameters.MaxLenghtOfTwoHundred)
            };
            //Profile
            yield return new object[]
            {
                new UserDto
                {
                    Name = "teste",
                    Email = "test@mail.com",
                    Password = "test123",
                    Profile = null
                },
                DomainError.ProfileIsRequired
            };
            //Email
            yield return new object[]
            {
                new UserDto
                {
                    Name = "test",
                    Email = null,
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                DomainError.EmailIsRequired
            };
            yield return new object[]
            {
                new UserDto
                {
                    Name = "test",
                    Email = string.Empty,
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                DomainError.EmailIsRequired
            };
            yield return new object[]
            {
                new UserDto
                {
                    Name = "test",
                    Email = StringExtension.GenerateString(DomainParameters.MaxLenghtOfTwoHundred + 1),
                    Password = "test123",
                    Profile = ProfileType.Administrator
                },
                string.Format(DomainError.MaximumEmailSize, DomainParameters.MaxLenghtOfTwoHundred)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
