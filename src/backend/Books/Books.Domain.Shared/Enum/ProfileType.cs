using System.ComponentModel.DataAnnotations;
using Books.Domain.Shared.Resources;

namespace Books.Domain.Shared.Enum
{
    public enum ProfileType : short
    {
        [Display(Name = nameof(Resource.Standard), ResourceType = typeof(Resource))]
        Standard = 1,
        [Display(Name = nameof(Resource.Administrator), ResourceType = typeof(Resource))]
        Administrator = 2
    }
}
