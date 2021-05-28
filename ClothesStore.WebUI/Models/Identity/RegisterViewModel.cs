using System.ComponentModel.DataAnnotations;

namespace ClothesStore.WebUI.Models.Identity
{
    public enum Role
    {
        User = 0,
        Admin = 1,
        Manager = 2
    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Повне ім'я")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        public string PasswordConfirm { get; set; }
        public Role Role { get; set; }

    }
}
