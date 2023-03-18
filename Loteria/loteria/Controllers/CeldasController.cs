using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using loteria.Models.Context;
using loteria.Models.Entities;

namespace loteria.Controllers
{
    public class CeldasController : Controller
    {
        private readonly DBContext _context;

        public CeldasController(DBContext context)
        {
            _context = context;
        }

        // GET: Celdas
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Celdas.Include(c => c.IdCartaNavigation).Include(c => c.IdTableroNavigation);
            return View(await dBContext.ToListAsync());
        }

        // GET: Celdas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Celdas == null)
            {
                return NotFound();
            }

            var celdas = await _context.Celdas
                .Include(c => c.IdCartaNavigation)
                .Include(c => c.IdTableroNavigation)
                .FirstOrDefaultAsync(m => m.IdCelda == id);
            if (celdas == null)
            {
                return NotFound();
            }

            return View(celdas);
        }

        // GET: Celdas/Create
        public IActionResult Create()
        {
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta");
            ViewData["IdTablero"] = new SelectList(_context.Tableros, "IdTablero", "IdTablero");
            return View();
        }

        // POST: Celdas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCelda,IdCarta,IdTablero,Fila,Columna")] Celdas celdas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(celdas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", celdas.IdCarta);
            ViewData["IdTablero"] = new SelectList(_context.Tableros, "IdTablero", "IdTablero", celdas.IdTablero);
            return View(celdas);
        }

        // GET: Celdas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Celdas == null)
            {
                return NotFound();
            }

            var celdas = await _context.Celdas.FindAsync(id);
            if (celdas == null)
            {
                return NotFound();
            }
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", celdas.IdCarta);
            ViewData["IdTablero"] = new SelectList(_context.Tableros, "IdTablero", "IdTablero", celdas.IdTablero);
            return View(celdas);
        }

        // POST: Celdas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCelda,IdCarta,IdTablero,Fila,Columna")] Celdas celdas)
        {
            if (id != celdas.IdCelda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(celdas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CeldasExists(celdas.IdCelda))
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
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", celdas.IdCarta);
            ViewData["IdTablero"] = new SelectList(_context.Tableros, "IdTablero", "IdTablero", celdas.IdTablero);
            return View(celdas);
        }

        // GET: Celdas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Celdas == null)
            {
                return NotFound();
            }

            var celdas = await _context.Celdas
                .Include(c => c.IdCartaNavigation)
                .Include(c => c.IdTableroNavigation)
                .FirstOrDefaultAsync(m => m.IdCelda == id);
            if (celdas == null)
            {
                return NotFound();
            }

            return View(celdas);
        }

        // POST: Celdas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Celdas == null)
            {
                return Problem("Entity set 'DBContext.Celdas'  is null.");
            }
            var celdas = await _context.Celdas.FindAsync(id);
            if (celdas != null)
            {
                _context.Celdas.Remove(celdas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CeldasExists(int id)
        {
          return (_context.Celdas?.Any(e => e.IdCelda == id)).GetValueOrDefault();
        }
    }
}
