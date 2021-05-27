using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClothesStore.WebUI.Models
{
    public class Cart
    {
        private List<ClothesOrder> lineCollection = new List<ClothesOrder>();

        public virtual void AddItem(ClothesMark product, int quantity)
        {
            var items = lineCollection.Select(p => new { p.ClothesUnit, p.Id });

            var line = items.Where(p => p.ClothesUnit.ClothesId == product.ClothesId && p.ClothesUnit.SizeId == product.SizeId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new ClothesOrder
                {
                    ClothesUnit = product,
                    CostPerSingle = product.Clothes.Cost,
                    Count = quantity
                });
            }
            else
            {
                lineCollection.FirstOrDefault(e => e.Id == line.Id).Count += 1;
            }
        }

        public virtual void RemoveLine(ClothesMark product) =>
            lineCollection.RemoveAll(l => l.ClothesUnit.Id == product.Id);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => (e.CostPerSingle-e.CostPerSingle*((decimal)e.ClothesUnit.Clothes.PromoutionPercent/100)) * e.Count);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<ClothesOrder> Lines => lineCollection;
    }
}
