using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Extensions;
using ClothesStore.WebUI.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Services
{
    public enum Role
    {
        User = 0,
        Admin = 1,
        Manager = 2
    }
    public class IdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAsyncRepository<Client> _clients;
        private readonly IAsyncRepository<Manager> _managers;

        public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<IdentityResult> RegisterUser(RegisterViewModel model, bool signIn, HttpContext context)
        {
            User user = new User { Email = model.Email, UserName = model.Email, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (model.Role > 0 && context.CheckFullPrivilegies())
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("access", model.Role.ToString()));

            if (result.Succeeded)
                if (signIn)
                    await _signInManager.SignInAsync(user, false);
            return result;
        }

        public async Task<SignInResult> LogIn(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
