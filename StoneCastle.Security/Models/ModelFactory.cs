using Microsoft.AspNet.Identity.EntityFramework;
using StoneCastle.Domain.Authentication.Entities;
using StoneCastle.WebSecurity.Models;

namespace StoneCastle.WebSecurity
{
    public class ModelFactory
    {
        private ApplicationUserManager _userManager;

        public ModelFactory(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public UserReturn Create(ApplicationUser appUser)
        {
            return new UserReturn
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                ChangePassword = appUser.ChangePassword,
                Roles = _userManager.GetRolesAsync(appUser.Id).Result,
                Claims = _userManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        public RoleReturn Create(IdentityRole appRole)
        {

            return new RoleReturn
            {
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
    }
}
