using System;
using Books.Domain.DTO;
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
        }
    }
}
