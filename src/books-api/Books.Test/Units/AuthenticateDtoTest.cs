using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Books.Domain.DTO;
using Books.Domain.Shared.Resources;
using FluentAssertions;
using Xunit;

namespace Books.Test.Units
{
    public class AuthenticateDtoTest
    {
        [Theory]
        [ClassData(typeof(AuthenticateDataTest))]
        public void UserDto_is_not_valid(AuthenticateDto dto, string message)
        {
            var result = dto.IsValid();

            result.Should().BeFalse();
            dto.ValidationResult.Errors.Should().NotBeNullOrEmpty();
            dto.ValidationResult.Errors.First().ErrorMessage.Should().Be(message);
        }

        [Fact]
        public void UserDto_is_valid()
        {
            var dto = new AuthenticateDto
            {
                Email = "test@mail.com",
                Password = "test123",
            };

            var result = dto.IsValid();

            result.Should().BeTrue();

            dto.ValidationResult.Errors.Should().BeNullOrEmpty();
        }
    }

    public class AuthenticateDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Email
            yield return new object[]
            {
                new AuthenticateDto
                {
                    Email = null,
                    Password = "test123",
                },
                DomainError.EmailIsRequired
            };
            yield return new object[]
            {
                new AuthenticateDto
                {
                    Email = string.Empty,
                    Password = "test123",
                },
                DomainError.EmailIsRequired
            };
            //Password
            yield return new object[]
            {
                new AuthenticateDto
                {
                    Email = "test@mail.com",
                    Password = null,
                },
                DomainError.PasswordIsRequired
            };
            yield return new object[]
            {
                new AuthenticateDto
                {
                    Email = "test@mail.com",
                    Password = string.Empty,
                },
                DomainError.PasswordIsRequired
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
