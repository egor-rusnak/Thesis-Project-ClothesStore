using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class SizeMarksService:ISizeMarksService
    {
        private readonly IAsyncRepository<Size> _sizes;
        private readonly IAsyncRepository<ClothesMark> _marks;


        public SizeMarksService(IAsyncRepository<Size> sizes, IAsyncRepository<ClothesMark> marks)
        {
            _sizes = sizes;
            _marks = marks;
        }


        public async Task AddMark(ClothesMark mark)
        {
            var markLikeThat = await _marks.GetBy(e => e.SizeId == mark.SizeId && e.ClothesId==mark.ClothesId);
            if (markLikeThat.Any()) throw new ArgumentException("Не можна додати такий розмір, він вже є у базі!");

            await _marks.Create(mark);
        }

        public async Task EditMark(ClothesMark mark)
        {
            var marksLikeThat = await _marks.GetBy(e => e.SizeId == mark.SizeId && e.ClothesId == mark.ClothesId);
            if (marksLikeThat.Count() > 1) throw new ArgumentException("Не можна змінити на цей розмір, він вже є для цього одягу!");

            await _marks.Update(mark);
        }
        
    }
}
