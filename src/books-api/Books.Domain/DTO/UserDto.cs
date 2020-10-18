using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Models;
using Books.Domain.Validation;
using FluentValidation.Results;

namespace Books.Domain.DTO
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ProfileType? Profile { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            var validation = new UserValidation();
            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
