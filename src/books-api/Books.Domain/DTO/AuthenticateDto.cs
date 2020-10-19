using Books.Domain.Shared.Models;
using Books.Domain.Validation;

namespace Books.Domain.DTO
{
    public class AuthenticateDto : BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            var validation = new AuthenticateValidation();
            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
