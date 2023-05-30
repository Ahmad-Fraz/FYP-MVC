using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class UserClaims : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public UserClaims(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Profile_Photo_Path", user.Profile_Photo_Path ?? ""));
            identity.AddClaim(new Claim("Degree", user.Degree ?? ""));
            identity.AddClaim(new Claim("Member_Type", user.Member_Type ?? ""));
            identity.AddClaim(new Claim("FullName", user.FullName ?? ""));
            identity.AddClaim(new Claim("Email", user.Email ?? ""));
            identity.AddClaim(new Claim("PhoneNo", user.PhoneNumber ?? ""));
            identity.AddClaim(new Claim("TwoFactorAuthentication", user.TwoFactorEnabled.ToString() ?? ""));
            identity.AddClaim(new Claim("id", user.Id ?? ""));
            identity.AddClaim(new Claim("Degree", user.Degree ?? ""));
            identity.AddClaim(new Claim("Home_Address", user.Home_Address ?? ""));
            identity.AddClaim(new Claim("City_Name", user.City_Name ?? ""));
            identity.AddClaim(new Claim("Two_step_Verification_Phone", user.Two_step_Verification_Phone ?? ""));
            identity.AddClaim(new Claim("Recovery_Email", user.Recovery_Email ?? ""));
            identity.AddClaim(new Claim("Gender", user.Gender ?? ""));
            identity.AddClaim(new Claim("DOB", user.DOB.ToString() ?? ""));

            return identity;
        }
    }
}
