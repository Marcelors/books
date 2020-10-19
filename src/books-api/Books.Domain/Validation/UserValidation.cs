using System;
using Books.Domain.DTO;
using Books.Domain.Shared.Parameters;
using Books.Domain.Shared.Resources;
using FluentValidation;

namespace Books.Domain.Validation
{
    public class UserValidation : AbstractValidator<UserDto>
    {
        public UserValidation()
        {
            RuleFor(x => x.Profile)
                .NotNull()
                .WithMessage(DomainError.ProfileIsRequired);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage(DomainError.NameIsRequired)
                .NotEmpty()
                .WithMessage(DomainError.NameIsRequired)
                .MaximumLength(DomainParameters.MaxLenghtOfTwoHundred)
                .WithMessage(string.Format(DomainError.MaximumNameSize, DomainParameters.MaxLenghtOfTwoHundred));

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage(DomainError.EmailIsRequired)
                .NotEmpty()
                .WithMessage(DomainError.EmailIsRequired)
                .MaximumLength(DomainParameters.MaxLenghtOfTwoHundred)
                .WithMessage(string.Format(DomainError.MaximumEmailSize, DomainParameters.MaxLenghtOfTwoHundred));
        }
    }
}
