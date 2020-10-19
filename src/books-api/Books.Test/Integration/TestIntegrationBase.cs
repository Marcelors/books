using System;
using AutoMapper;
using Books.Domain.Authentication;
using Books.Domain.Interfaces;
using Books.Domain.Shared.Nofication;
using Books.Infra.CrossCutting.IoC;
using Books.Infra.Data.Context;
using Books.Test.Fake;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Test.Integration
{
    public class TestIntegrationBase : IDisposable
    {
        protected IServiceCollection Service;
        private SqliteConnection _connection;
        protected BookContext DbContext;
        protected DomainNotificationHandler DomainNotificationHandler = new DomainNotificationHandler();
        private IServiceScope _scope;

        public TestIntegrationBase()
        {
            OpenConnection();
            Service = new ServiceCollection();

            var tokenConfiguration = new TokenConfiguration
            {
                Audience = "test",
                Hours = 1,
                Issuer = "test"
            };
            
            Service.AddSingleton<ITokenConfiguration>(tokenConfiguration);

            var signingConfiguration = new SigningConfiguration();
            signingConfiguration.GenerateKey();
            Service.AddSingleton<ISigningConfiguration>(signingConfiguration);

            Service.AddAutoMapper(typeof(TestIntegrationBase));
            Service.Register();
            Service.AddScoped(provider => DbContext);
            Service.AddScoped<IMediator, MediatorFake>();
            Service.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>(provider => DomainNotificationHandler);
        }

        private void OpenConnection()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BookContext>()
                    .UseSqlite(_connection)
                    .Options;

            DbContext = new BookContext(options);
            DbContext.Database.EnsureCreated();
        }

        protected void CreateScope()
        {
            var sp = Service.BuildServiceProvider();
            _scope = sp.CreateScope();
        }

        protected T GetIntanceScope<T>()
        {
            var sp = _scope?.ServiceProvider;
            if (sp == null)
            {
                return default(T);
            }
            return sp.GetService<T>();
        }

        protected T GetInstance<T>()
        {
            var sp = Service.BuildServiceProvider();
            return sp.GetService<T>();
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}
