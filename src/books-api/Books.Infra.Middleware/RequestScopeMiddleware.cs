using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Books.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Books.Infra.Middleware
{
    public class RequestScopeMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestScopeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IRequestScope requestScope)
        {
            var token = httpContext.Request?.Headers["Authorization"];
            var endpoint = httpContext.Features?.Get<IEndpointFeature>()?.Endpoint;


            var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>();

            if (allowAnonymous != null)
            {
                return _next(httpContext);
            }

            if (!token.HasValue || !token.Value.Any())
            {
                return _next(httpContext);
            }


            var userId = httpContext.User.GetValue("UserId");

            return _next(httpContext);
        }
    }
}
