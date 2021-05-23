using Microsoft.AspNetCore.Identity;

namespace ClothesStore.WebUI.Models.Identity
{
    public class User : IdentityUser
    {
        public int IdForExternalDb { get; set; }
    }
}
