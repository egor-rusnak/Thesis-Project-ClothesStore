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
            _timer.Interval = 30000;
            _timer.Start();
            Service_Elapsed(this, null);
        }

        private void Service_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var clothesService = scope.ServiceProvider.GetRequiredService<IClothesRepository>();
                var ordersService = scope.ServiceProvider.GetRequiredService < IAsyncRepository<Order>>();
                var orders = ordersService.GetAll().Result.Where(e=>e.DateOfOrder>=DateTime.Now.AddMonths(-1) && e.Shiped).ToList();
                if (orders.Count() == 0)
                {
                    clothesList = new List<Clothes>();
                    return;
                }
                var allOrderedItems = orders.SelectMany(e => e.ClothesOrders);
                var resultItems = allOrderedItems.Select(e => e.ClothesUnit.Clothes);
                var elems = clothesService.GetClothesWithSizesAndBrands().Result.Intersect(resultItems);
                clothesList = elems.ToList();
            }
        }

        public IEnumerable<Clothes> GetPopularByLastMonth(int count)
        {
            return clothesList?.Take(count);
        }
    }
}
