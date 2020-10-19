using System;
using System.Security.Claims;
using Books.Domain.Shared.Extensions;

namespace Books.Infra.Middleware
{
    public static class ClaimsPrincipalExtenstion
    {
        public static string GetValue(this ClaimsPrincipal claimsPrincipal, string key)
        {
            if (claimsPrincipal == null) return string.Empty;

            var claim = claimsPrincipal.FindFirst(key);

            if (claim == null)
            {
                return null;
            }

            if (!claim.Value.HasValue())
            {
                return null;
            }

            return claim.Value;
        }
    }
}
