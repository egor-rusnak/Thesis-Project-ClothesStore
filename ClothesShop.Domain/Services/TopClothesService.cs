using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClothesStore.Domain.Services
{
    public class TopClothesService : ITopClothesService
    {
        private IEnumerable<Clothes> clothesList = new List<Clothes>();

        private readonly ITimerService _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public TopClothesService(IServiceScopeFactory factory, ITimerService timer)
        {
            _scopeFactory = factory;
            _timer = timer;
            _timer.Elapsed += Service_Elapsed;
            _timer.Interval = 10000;
            _timer.Start();
        }

        private void Service_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var orders = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Order>>().GetBy(e => e.Shiped && e.DateOfOrder >= DateTime.Now.AddMonths(-1)).Result;
                if (orders.Count() == 0) clothesList = new List<Clothes>();

                var clothes = orders.SelectMany(e => e.ClothesOrders);
                var resClothes = clothes.Select(e => e.ClothesUnit);
                var resresClothes = resClothes.Select(e => e.Clothes).ToList();
                resresClothes.GroupBy(e => e.Id).OrderByDescending(e=>e.Count());
            }
        }

        public IEnumerable<Clothes> GetPopularByLastMonth(int count)
        {
            return clothesList?.Take(count);
        }
    }
}
