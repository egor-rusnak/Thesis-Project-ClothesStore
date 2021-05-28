using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Extensions;
using ClothesStore.WebUI.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Services
{
    public class IdentityService
    {
        private const string AdminLogin = "Admin12345";
        private const string AdminPass = "n1313213N";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAsyncRepository<Client> _clients;
        private readonly IAsyncRepository<Manager> _managers;

        public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager, IAsyncRepository<Client> clients, IAsyncRepository<Manager> managers)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _clients = clients;
            _managers = managers;
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel model, bool signIn, HttpContext context)
        {
            User user = new User { Email = model.Login, UserName = model.Login, FullName = model.FullName,  PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (model.Role > 0 && context.CheckFullPrivilegies())
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("access", model.Role.ToString()));
            if (model.Role == Role.Manager)
            {
                var manager = await _managers.Create(new Manager());

                user.IdManager = manager.Id;
                await _userManager.UpdateAsync(user);
            }
            else if (model.Role == Role.User)
            {
                var client = await _clients.Create(new Client { Name = user.FullName, PhoneNumber = user.PhoneNumber });
                
                user.IdClient = client.Id;
                await _userManager.UpdateAsync(user);
            }
            else if (model.Role == Role.Admin)
            {
                var client = await _clients.Create(new Client { Name = user.FullName, PhoneNumber = user.PhoneNumber });
                var manager = await _managers.Create(new Manager());
                user.IdClient = client.Id;
                user.IdManager = manager.Id;
                await _userManager.UpdateAsync(user);
            }

            if (result.Succeeded)
                if (signIn)
                    await _signInManager.SignInAsync(user, false);
            return result;
        }

        public async Task<SignInResult> LogIn(LoginViewModel model)
        {
            var resultUsers = await _userManager.GetUsersForClaimAsync(new System.Security.Claims.Claim("access", Role.Admin.ToString()));
            if (resultUsers.Count == 0)
            {
                User user = new User { UserName = AdminLogin, PhoneNumber="NoNum" };
                var created = await _userManager.CreateAsync(user, "n123321N");
                var resultClaim = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("access", Role.Admin.ToString()));
                var client = await _clients.Create(new Client());
                var manager = await _managers.Create(new Manager());
                user.IdClient = client.Id;
                user.IdManager = manager.Id;

                await _userManager.UpdateAsync(user);
                
                if (created.Succeeded)
                    Console.WriteLine("Created a admin user for first time with username: " + user.UserName + " and pass: n123321N");

                if (resultClaim.Succeeded)
                    Console.WriteLine("Added admin claim for it!");
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
