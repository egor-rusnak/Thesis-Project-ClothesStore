using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class CreateViewModel<T>
    {
        public T Entity { get; set; }
        [DisplayName("Зображення")]
        public IFormFile Image { get; set; }
        public string ReturnUrl { get; set; }
    }
}
