using Microsoft.AspNetCore.Http;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class EditViewModel<T>
    {
        public T Entity { get; set; }
        public IFormFile Image { get; set; }
        public string ReturnUrl { get; set; }
    }
}
