using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orders;

        public OrderService(IAsyncRepository<Order> orders)
        {
            _orders = orders;
        }

        public async Task AddOrder(Order order)
        {
            await _orders.Create(order);
        }

        public async Task CancelOrder(int id)
        {
            var order = await _orders.GetById(id);
            order.Canceled = true;
            await _orders.Update(order);
        }

        public async Task<IEnumerable<Order>> GetLastClientOrders(int count, int clientId)
        {
            var orders = (await _orders.GetBy(e => e.ClientId == clientId)).Take(count);
            return orders;
        }

        public async Task RemoveOrder(int id)
        {
            await _orders.Delete(id);
        }

        public async Task UpdateOrder(Order order)
        {
            await _orders.Update(order);
        }
    }
}
