using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order, IEnumerable<ClothesOrder> list);
        Task RemoveOrder(int id);
        Task<IEnumerable<Order>> GetLastClientOrders(int count, int clientId);
        Task UpdateOrder(Order order);
        Task CancelOrder(int id);
        Task<IEnumerable<ClothesMark>> UnOrderableMarks(IEnumerable<ClothesOrder> list);
        Task<IEnumerable<Order>> GetOrdersWithManagerIdOrWithoutManager(int managerId);
    }
}
