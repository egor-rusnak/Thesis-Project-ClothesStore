using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public enum PaymentMethod
    {
        Отримання,
        Передоплата
    }
    public class Order : TEntity
    {
        [System.ComponentModel.DisplayName("Метод оплати")]
        [Required]
        public PaymentMethod PayMethod { get; set; }

        [DataType(DataType.Date)]
        [System.ComponentModel.DisplayName("Поточний час замовлення")]
        public DateTime DateOfOrder { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [System.ComponentModel.DisplayName("Примірний час отримання")]
        public DateTime ShipDate { get; set; }
        public virtual Client Client { get; set; }

        public virtual Manager Manager { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [System.ComponentModel.DisplayName("Поштовий код")]
        public string PostalCode { get;  set; }
        [Required]
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Місто")]
        public string City { get;  set; }
        [Required]
        [StringLength(500)]
        [System.ComponentModel.DisplayName("Адреса")]
        public string ShipAddress { get;  set; }

        public virtual IEnumerable<ClothesOrder> ClothesOrders { get; set; }

        [System.ComponentModel.DisplayName("Відмінено")]
        public bool Canceled { get; set; } = false;

        [System.ComponentModel.DisplayName("Доставку та оплату виконано")]
        public bool Shiped { get; set; } = false;

    }
}
