
using ClothesStore.WebUI.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace ClothesStore.WebUI.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool CheckFullPrivilegies(this HttpContext context)
        {
            if (context.User.HasClaim("access", Role.Admin.ToString()))
                return true;
            else
                return false;
        }
        public static bool CheckManagerPrivilegies(this HttpContext context)
        {
            if (context.User.HasClaim("access", Role.Admin.ToString()) || context.User.HasClaim("access", Role.Manager.ToString()))
                return true;
            else
                return false;
        }
    }
}
