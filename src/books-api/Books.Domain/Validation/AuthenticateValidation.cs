using System;
using Books.Domain.DTO;
using Books.Domain.Shared.Resources;
using FluentValidation;

namespace Books.Domain.Validation
{
    public class AuthenticateValidation : AbstractValidator<AuthenticateDto>
    {
        public AuthenticateValidation()
        {
            RuleFor(x => x.Email)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .WithMessage(DomainError.EmailIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.EmailIsRequired);

            RuleFor(x => x.Password)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .WithMessage(DomainError.PasswordIsRequired)
               .NotEmpty()
               .WithMessage(DomainError.PasswordIsRequired);
        }
    }
}
