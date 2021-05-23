using ClothesStore.Domain.Entities;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task RemoveOrder(Order order);
        Task GetLastClientOrders(int count, int clientId);
        Task UpdateOrder(Order order);
        Task ChechoutOrder(int id);
        Task CancelOrder(int id);
    }
}
