
using ClothesStore.Domain.Entities;
using ClothesStore.WebUI.Models.Identity;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

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
        public static async Task WriteImageClothes(this HttpContext context, IWebHostEnvironment env, CreateClothesViewModel model)
        {
            var clothes = model.Entity;
            foreach (var Image in context.Request.Form.Files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                   
                    var uploads = Path.Combine(env.WebRootPath, "uploads\\clothes");
                    if (file.Length > 0)
                    {
                        var fileName = clothes.Id + "_" + clothes.Name.Replace("/","_").Replace("\\","_") + Path.GetExtension(file.FileName);
                        if (!Directory.Exists(uploads))
                            Directory.CreateDirectory(uploads);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            clothes.ImageName = fileName;
                        }
                    }
                }
            }
        }
        public static async Task WriteImageTypes(this HttpContext context, IWebHostEnvironment env, CreateViewModel<ClothesType> model)
        {
            var type = model.Entity;
            foreach (var Image in context.Request.Form.Files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    var uploads = Path.Combine(env.WebRootPath, "uploads\\types");
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);
                    if (file.Length > 0)
                    {
                        var fileName = type.Id + "_" + type.Name.Replace("/", "\\").Replace("\\", "_") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            type.ImageName = fileName;
                        }
                    }
                }
            }
        }
    }
}
