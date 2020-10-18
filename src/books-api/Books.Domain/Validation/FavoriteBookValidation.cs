using System;
using Books.Domain.DTO;
using Books.Domain.Shared.Parameters;
using Books.Domain.Shared.Resources;
using FluentValidation;

namespace Books.Domain.Validation
{
    public class FavoriteBookValidation : AbstractValidator<FavoriteBookDto>
    {
        public FavoriteBookValidation()
        {
            RuleFor(x => x.Authors)
               .NotNull()
               .WithMessage(DomainError.AuthorsIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.AuthorsIsRequired);

            RuleFor(x => x.BookId)
               .NotNull()
               .WithMessage(DomainError.BookIdIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.BookIdIsRequired)
               .MaximumLength(DomainParameters.MaxLenghtOfFifty)
               .WithMessage(string.Format(DomainError.MaximumBookIdSize, DomainParameters.MaxLenghtOfFifty));

            RuleFor(x => x.Link)
               .NotNull()
               .WithMessage(DomainError.LinkIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.LinkIsRequired)
               .MaximumLength(DomainParameters.MaxLenghtOfOneThousand)
               .WithMessage(string.Format(DomainError.MaximumLinkSize, DomainParameters.MaxLenghtOfOneThousand));

            RuleFor(x => x.Title)
               .NotNull()
               .WithMessage(DomainError.TitleIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.TitleIsRequired)
               .MaximumLength(DomainParameters.MaxLenghtOfFourHundred)
               .WithMessage(string.Format(DomainError.MaximumTitleSize, DomainParameters.MaxLenghtOfFourHundred));

            RuleFor(x => x.User)
               .NotNull()
               .WithMessage(DomainError.UserIsRequired);
        }
    }
}
