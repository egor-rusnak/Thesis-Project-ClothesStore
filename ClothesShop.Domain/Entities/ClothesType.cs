using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Entities
{
    public enum ClothesDestinantion
    {
        Men,
        Women,
        Children
    }
    public class ClothesType:TEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        public ClothesDestinantion Destinantion { get; set; } 
        public string imgSrc { get; set; }
        public IEnumerable<Clothes> Clothes { get;set; }
    }
}
