namespace GrowATree.WebAPI.Services
{
    using System.Security.Claims;
    using GrowATree.Application.Common.Interfaces;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Service that returns the current logged user.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Current HTTP context.</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Gets the current logged user id.
        /// </summary>
        public string UserId { get; }
    }
}