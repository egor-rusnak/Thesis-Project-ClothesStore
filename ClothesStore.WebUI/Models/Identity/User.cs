using Microsoft.AspNetCore.Identity;

namespace ClothesStore.WebUI.Models.Identity
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public int IdManager { get; set; }
        public int IdClient { get; set; }
    }
}
