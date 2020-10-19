using System;
using Books.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Books.Infra.CrossCutting.IoC;
using AutoMapper;
using Books.ApplicationService.AutoMapper;
using Books.Infra.Middleware;
using Books.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Books.Domain.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;

namespace Books.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
      
            services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            var tokenConfiguration = new TokenConfiguration();
            Configuration.GetSection("TokenConfigurations").Bind(tokenConfiguration);
            services.AddSingleton<ITokenConfiguration>(tokenConfiguration);

            var signingConfiguration = new SigningConfiguration();
            signingConfiguration.GenerateKey();
            services.AddSingleton<ISigningConfiguration>(signingConfiguration);

            services.AddCors(options =>
            {
                options.AddPolicy("policy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.SetIsOriginAllowed(_ => true);
                    builder.WithExposedHeaders("Content-Disposition");
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingConfiguration.Key,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidIssuer = tokenConfiguration.Issuer,
                     ValidAudience = tokenConfiguration.Audience,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });



            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionHandlingFilter));
                options.Filters.Add(new AuthorizeFilter("Bearer"));
            }).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
              .AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });


            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
            services.Register();
            services.AddMediatR(typeof(Startup));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("policy");
       
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting()
               .UseLocalizationMiddleware()
               .UseRequestScopeMiddleware(); ;

          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
