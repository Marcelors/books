using AutoMapper;
using Books.ApplicationService.Model;
using Books.Domain.DTO;
using Books.Domain.Entities;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.AutoMapper
{
    public class DomainToModelProfile : Profile
    {
        public DomainToModelProfile()
        {
            CreateMap<BookDto, BookModel>();
            CreateMap<VolumeInfoDto, VolumeInfoModel>();
            CreateMap<ImageLinksDto, ImageLinksModel>();

            CreateMap<User, UserModel>()
                .ForMember(x => x.Profile, m => m.MapFrom(a => (short)a.Profile))
                .ForMember(x => x.ProfileModel, m => m.MapFrom(a => new EnumModel<short>((short)a.Profile, a.Profile.GetDescription())));

            CreateMap<FavoriteBook, FavoriteBookModel>();
        }
    }
}
