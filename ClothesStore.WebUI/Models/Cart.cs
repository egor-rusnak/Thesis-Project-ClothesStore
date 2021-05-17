using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models
{
    public class Cart
    {
        private List<ClothesOrder> lineCollection = new List<ClothesOrder>();

        public virtual void AddItem(ClothesMark product, int quantity)
        {
            ClothesOrder line = lineCollection.Where(p => p.ClothesUnit.ClothesId== product.ClothesId && p.ClothesUnit.SizeId==product.SizeId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new ClothesOrder
                {
                    ClothesUnit = product,
                    CostPerSingle = product.Cost,
                    Count = quantity
                });
            }
            else
            {
                line.Count += 1;
            }
        }

        public virtual void RemoveLine(ClothesMark product) =>
            lineCollection.RemoveAll(l => l.ClothesUnit == product);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.CostPerSingle * e.Count);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<ClothesOrder> Lines => lineCollection;
    }
}
