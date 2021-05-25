﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesStore.Domain.Entities;
using ClothesStore.Infrastructure.Data;
using ClothesStore.WebUI.Models.ViewModels;

namespace ClothesStore.WebUI.Controllers
{
    public class TypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Types
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothesTypes.ToListAsync());
        }

        // GET: Types/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesType = await _context.ClothesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothesType == null)
            {
                return NotFound();
            }

            return View(clothesType);
        }

        // GET: Types/Create
        public IActionResult Create(string returnUrl)
        {
            return View(new CreateViewModel<ClothesType>() { ReturnUrl = returnUrl });
        }

        // POST: Types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel<Type> model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model.Entity);
                await _context.SaveChangesAsync();
                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index");
                else return Redirect(model.ReturnUrl);
            }
            return View(model.Entity);
        }

        // GET: Types/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesType = await _context.ClothesTypes.FindAsync(id);
            if (clothesType == null)
            {
                return NotFound();
            }
            return View(clothesType);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Destinantion,ImageName,Id")] ClothesType clothesType)
        {
            if (id != clothesType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothesTypeExists(clothesType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clothesType);
        }

        // GET: Types/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothesType = await _context.ClothesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothesType == null)
            {
                return NotFound();
            }

            return View(clothesType);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothesType = await _context.ClothesTypes.FindAsync(id);
            _context.ClothesTypes.Remove(clothesType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesTypeExists(int id)
        {
            return _context.ClothesTypes.Any(e => e.Id == id);
        }
    }
}