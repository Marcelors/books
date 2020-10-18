using System.Collections.Generic;
using Books.Domain.Entities;
using Books.Domain.Shared.Models;
using Books.Domain.Validation;
using FluentValidation.Results;

namespace Books.Domain.DTO
{
    public class FavoriteBookDto : BaseDto
    {
        public string BookId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public IList<string> Authors { get; set; } = new List<string>();
        public User User { get; set; }

        public bool IsValid() {
            var validation = new FavoriteBookValidation();
            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
