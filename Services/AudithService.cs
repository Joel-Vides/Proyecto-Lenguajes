using Terminal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Terminal.Services
{
    public class AudithService : IAudithService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AudithService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
                return "SYSTEM"; 

            var userIdClaim = httpContext.User.Claims
                .FirstOrDefault(x => x.Type == "UserId");

            return userIdClaim?.Value ?? "SYSTEM";
        }
    }
}
