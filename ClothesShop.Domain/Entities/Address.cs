using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    [Owned]
    public class Address
    {
        [Required]
        [DataType(DataType.PostalCode)]
        [System.ComponentModel.DisplayName("Поштовий код")]
        public string PostalCode { get; private set; }
        [Required]
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Місто")]
        public string City { get; private set; }
        [Required]
        [StringLength(500)]
        [System.ComponentModel.DisplayName("Адреса")]
        public string ShipAddress { get; private set; }

        public Address(string shipAddress, string city, string postalCode)
        {
            ShipAddress = shipAddress;
            City = city;
            PostalCode = postalCode;
        }
    }
}