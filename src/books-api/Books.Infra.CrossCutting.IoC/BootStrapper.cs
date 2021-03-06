﻿using AutoMapper;
using Books.ApplicationService.Application;
using Books.ApplicationService.AutoMapper;
using Books.ApplicationService.Inferfaces;
using Books.Domain.Authentication;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Services;
using Books.Domain.Shared.Nofication;
using Books.Infra.Data.Context;
using Books.Infra.Data.Repositories;
using Books.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Infra.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfig.RegisterMappings());
            services.AddSingleton<ITokenEncoder, TokenEncoder>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<IBookApplicationService, BookApplicationService>();
            services.AddScoped<IFavoriteBookApplicationService, FavoriteBookApplicationService>();
            services.AddScoped<IAuthenticateApplicationService, AuthenticateApplicationService>();

            services.AddScoped<IFavoriteBookService, FavoriteBookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFavoriteBookRepository, FavoriteBookRepository>();

            services.AddScoped<BookContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRequestScope, RequestScope>();
        }
    }
}
