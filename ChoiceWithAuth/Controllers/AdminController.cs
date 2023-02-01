using ChoiceWithAuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChoiceWithAuth.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await userManager
                .GetUsersForClaimAsync(new Claim("Admin", "Yes"));

            return View(admins);
        }

        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterAdminViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new IdentityUser(viewModel.Name);
            await userManager.CreateAsync(user, viewModel.Password);
            await userManager.AddClaimAsync(user, new Claim("Admin", "Yes"));

            return RedirectToAction(nameof(Index));
        }
    }
}
