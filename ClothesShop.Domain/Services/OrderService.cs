using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orders;
        private readonly IAsyncRepository<ClothesOrder> _orderItems;
        private readonly IAsyncRepository<ClothesMark> _store;
        private readonly IOrderRepository _orderRep;
        private readonly IAsyncRepository<Client> _clients;
        public OrderService(IAsyncRepository<Order> orders, IAsyncRepository<ClothesMark> store, IAsyncRepository<ClothesOrder> orderItems, IOrderRepository orderRep, IAsyncRepository<Client> clients)
        {
            _orders = orders;
            _store = store;
            _orderItems = orderItems;
            _orderRep = orderRep;
            _clients = clients;
        }

        public async Task AddOrder(Order order, IEnumerable<ClothesOrder> list)
        {
            if((await UnOrderableMarks(list)).Any())
                throw new ArgumentException("Не достатньо речей на складі!");

            order.ClothesOrders = list.ToList();
            await _orderRep.SaveOrder(order);
           
            foreach(var item in order.ClothesOrders)
            {
                item.ClothesUnit.CountInStock -= item.Count;
                await _store.Update(item.ClothesUnit);
            }
        }

        public async Task<IEnumerable<ClothesMark>> UnOrderableMarks(IEnumerable<ClothesOrder> list)
        {
            var elems = list.Select(e => new { e.ClothesUnit, e.Count, e.ClothesUnit.Size }).Where(e => e.ClothesUnit.CountInStock < e.Count)
                .Select(e => e.ClothesUnit);
            return elems;
        }

        public async Task CancelOrder(int id)
        {
            var order = await _orders.GetById(id);
            order.Canceled = true;
            await _orders.Update(order);
        }

        public async Task<IEnumerable<Order>> GetLastClientOrders(int count, int clientId)
        {
            var orders = (await _orders.GetBy(e => e.Client.Id == clientId)).Take(count);
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
