using AutoMapper;
using Books.ApplicationService.Model;
using Books.Domain.DTO;
using Books.Domain.Shared.Enums;

namespace Books.ApplicationService.AutoMapper
{
    public class ModelToDomainProfile : Profile
    {
        public ModelToDomainProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ForMember(x => x.Profile, m => m.MapFrom(a => a.Profile.HasValue ? (ProfileType)a.Profile.Value : (ProfileType?)null));

            CreateMap<FavoriteBookModel, FavoriteBookDto>();
            CreateMap<AuthenticateModel, AuthenticateDto>();
        }
    }
}
