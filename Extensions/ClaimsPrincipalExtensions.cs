using System.Security.Claims;

namespace Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        private static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            if (result != null) return result;
            return new List<string>();
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims(ClaimTypes.Role);
        }
    }
}
