using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class EfOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EfOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders => context.Orders.Include(o => o.ClothesOrders)
            .ThenInclude(l => l.ClothesUnit);

        public async Task SaveOrder(Order order)
        {
            context.AttachRange(order.ClothesOrders.Select(l => l.ClothesUnit));
            if (order.Id == 0)
            {
                context.Orders.Add(order);
            }
            await context.SaveChangesAsync();
        }
    }
}
