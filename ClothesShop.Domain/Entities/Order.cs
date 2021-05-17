using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Entities
{
    public enum PaymentMethod
    {
        InGet,
        BeforeGet
    }
    public class Order:TEntity
    {
        public PaymentMethod PayMethod { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfOrder { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }
        
        public bool IsPaid { get; set; } = false;
        
        public Client Client { get; set; }
        
        public Manager Manager { get; set; }
        
        public Address ShipAddress { get; private set; }
        
        public IEnumerable<ClothesOrder> ClothesOrders { get; set; }
        
        public bool Canceled { get; set; } = false;
        
        public bool Finished { get; set; } = false;

        public void SetAddress(string postalCode, string city, string shipAddress)
        {
            ShipAddress = new Address(shipAddress, city, postalCode);
        }
    }
}
