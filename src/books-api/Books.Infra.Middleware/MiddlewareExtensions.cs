using System;
using Microsoft.AspNetCore.Builder;

namespace Books.Infra.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }

        public static IApplicationBuilder UseRequestScopeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestScopeMiddleware>();
        }
    }
}
