using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface ISizeMarksService
    {
        Task AddMark(ClothesMark mark);

        Task EditMark(ClothesMark mark);
    }
}
