using GestionDesPresences.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GestionDesPresences.AI.Context
{
    public class AIUserContextService : IAIUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public AIUserContextService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<AIUserContext> GetCurrentAsync()
        {
            var principal = _httpContextAccessor.HttpContext?.User;

            if (principal == null)
                return new AIUserContext();

            var user = await _userManager.GetUserAsync(principal);

            if (user == null)
                return new AIUserContext();

            var roles = await _userManager.GetRolesAsync(user);

            return new AIUserContext
            {
                UserId = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                CollaborateurId = user.CollaborateurId,
                Role = roles.FirstOrDefault() ?? "",
                Culture = CultureInfo.CurrentCulture.Name,
                Language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            };
        }
    }
}
