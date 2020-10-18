using FluentValidation.Results;

namespace Books.Domain.Shared.Models
{
    public class BaseDto
    {
        public ValidationResult ValidationResult { get; set; }
    }
}
