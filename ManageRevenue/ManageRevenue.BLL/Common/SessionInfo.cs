using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ManageRevenue.BLL.Common
{
    public class SessionInfo : ISessionInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }

}
