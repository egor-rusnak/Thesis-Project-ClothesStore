using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Task SaveOrder(Order order);
    }
}
