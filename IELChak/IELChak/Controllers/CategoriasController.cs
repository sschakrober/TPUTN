﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IELChak.Data;
using IELChak.Models;
using IELChak.ModelsClass;
using Microsoft.AspNetCore.Identity;

namespace IELChak.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CategoriaModels categoriaModels;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
            categoriaModels = new CategoriaModels(_context);
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public List<IdentityError> guardarCategoria(string nombre, string descripcion, string estado)
        {
            return categoriaModels.guardarCategoria(nombre, descripcion, estado);
        }

        

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaID == id);
        }
    }
}
