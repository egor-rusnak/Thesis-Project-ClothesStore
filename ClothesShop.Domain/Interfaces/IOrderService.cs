﻿using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task RemoveOrder(int id);
        Task<IEnumerable<Order>> GetLastClientOrders(int count, int clientId);
        Task UpdateOrder(Order order);
        Task CancelOrder(int id);
    }
}
