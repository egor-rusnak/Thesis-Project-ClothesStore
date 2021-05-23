using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        public Task AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CancelOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task ChechoutOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetLastClientOrders(int count, int clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
