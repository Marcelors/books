using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Books.Infra.Middleware
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string[] _supportedLanguages = new[]
        {
            "pt-BR",
            "en-US",
        };

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext httpContext)
        {
            var userLanguage = httpContext.Request.Headers["Accept-Language"].ToString();
            var firstLanguage = userLanguage.Split(',').FirstOrDefault();

            if (firstLanguage == null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            }
            else if (!_supportedLanguages.Any(x => x.Equals(firstLanguage)))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(firstLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(firstLanguage);
            }

            return _next(httpContext);
        }
    }
}
