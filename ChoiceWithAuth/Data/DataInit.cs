using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ChoiceWithAuth.Data
{
    public static class DataInit
    {
        // Додати вбудованого admin'а
        //
        public static async void AddEmbeddedAdmin(this WebApplication app, string name, string password)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager != null)
            {
                IdentityUser user = new(name);
                await userManager.CreateAsync(user, password);
                await userManager.AddClaimAsync(user, new Claim("Admin", "Yes"));
                //user.EmailConfirmed = true;
            }
        }
    }
}
