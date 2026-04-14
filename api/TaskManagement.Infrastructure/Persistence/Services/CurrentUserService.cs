using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Infrastructure.Persistence.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(claim, out var id) ? id : null;
            }
        }
    }
}
