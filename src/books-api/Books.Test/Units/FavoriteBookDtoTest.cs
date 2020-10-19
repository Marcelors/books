using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Books.Domain.DTO;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Parameters;
using Books.Domain.Shared.Resources;
using Books.Test.Builder;
using FluentAssertions;
using Xunit;

namespace Books.Test.Units
{
    public class FavoriteBookDtoTest
    {
        [Theory]
        [ClassData(typeof(FavoriteBookDataTest))]
        public void FavoriteBookDto_is_not_valid(FavoriteBookDto dto, string message)
        {
            var result = dto.IsValid();

            result.Should().BeFalse();
            dto.ValidationResult.Errors.Should().NotBeNullOrEmpty();
            dto.ValidationResult.Errors.First().ErrorMessage.Should().Be(message);
        }

        [Fact]
        public void FavoriteBookDto_is_valid()
        {
            var dto = new FavoriteBookDto
            {
                Title = "test",
                Description = "test",
                Authors = new List<string>() { "test", "test2" },
                BookId = "test",
                Link = "test",
                Thumbnail = "test",
                User = new UserBuilder().Builder(),
            };

            var result = dto.IsValid();

            result.Should().BeTrue();

            dto.ValidationResult.Errors.Should().BeNullOrEmpty();
        }
    }

    public class FavoriteBookDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //user
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    BookId = "test",
                    Link = "test",
                    Thumbnail = "test",
                    User = null,
                },
                DomainError.UserIsRequired
            };
            //authors
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>(),
                    BookId = "test",
                    Link = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.AuthorsIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = null,
                    BookId = "test",
                    Link = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.AuthorsIsRequired
            };
            //BookId
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    BookId = null,
                    Link = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.BookIdIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    BookId = string.Empty,
                    Link = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.BookIdIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    BookId = StringExtension.GenerateString(DomainParameters.MaxLenghtOfFifty + 1),
                    Link = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                string.Format(DomainError.MaximumBookIdSize, DomainParameters.MaxLenghtOfFifty)
            };
            //Link
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Link = null,
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.LinkIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Link = string.Empty,
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.LinkIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Title = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Link = StringExtension.GenerateString(DomainParameters.MaxLenghtOfOneThousand + 1),
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                string.Format(DomainError.MaximumLinkSize, DomainParameters.MaxLenghtOfOneThousand)
            };
            //Title
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Link = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Title = null,
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.TitleIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Link = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Title = string.Empty,
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                DomainError.TitleIsRequired
            };
            yield return new object[]
            {
                new FavoriteBookDto
                {
                    Link = "test",
                    Description = "test",
                    Authors = new List<string>() {"test", "test2"},
                    Title = StringExtension.GenerateString(DomainParameters.MaxLenghtOfFourHundred + 1),
                    BookId = "test",
                    Thumbnail = "test",
                    User = new UserBuilder().Builder(),
                },
                string.Format(DomainError.MaximumTitleSize, DomainParameters.MaxLenghtOfFourHundred)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
