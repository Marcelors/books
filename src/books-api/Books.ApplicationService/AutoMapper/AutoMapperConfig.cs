using System;
using AutoMapper;

namespace Books.ApplicationService.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(x =>
            {
                x.AddProfile(new DomainToModelProfile());
                x.AddProfile(new ModelToDomainProfile());
            });
        }
    }
}
