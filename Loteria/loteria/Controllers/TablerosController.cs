using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using loteria.Models.Context;
using loteria.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using System.Data;

namespace loteria.Controllers
{
    public class TablerosController : Controller
    {
        private readonly DBContext _context;

        public TablerosController(DBContext context)
        {
            _context = context;
        }

        // GET: Tableros
        public async Task<IActionResult> Index()
        {
            var tableros = await _context.Tableros
                .Include(t => t.Celdas)
                    .ThenInclude(c => c.IdCartaNavigation)
                .ToListAsync();

            return View(tableros);
        }


        // GET: Tableros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tableros == null)
            {
                return NotFound();
            }

            var tableros = await _context.Tableros
                .FirstOrDefaultAsync(m => m.IdTablero == id);
            if (tableros == null)
            {
                return NotFound();
            }

            return View(tableros);
        }

        // GET: Tableros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tableros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTablero,Nombre,Descripcion")] Tableros tableros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tableros);
        }

        // GET: Tableros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tableros == null)
            {
                return NotFound();
            }

            var tableros = await _context.Tableros.FindAsync(id);
            if (tableros == null)
            {
                return NotFound();
            }
            return View(tableros);
        }

        // POST: Tableros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTablero,Nombre,Descripcion")] Tableros tableros)
        {
            if (id != tableros.IdTablero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablerosExists(tableros.IdTablero))
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
            return View(tableros);
        }

        // GET: Tableros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tableros == null)
            {
                return NotFound();
            }

            var tableros = await _context.Tableros
                .FirstOrDefaultAsync(m => m.IdTablero == id);
            if (tableros == null)
            {
                return NotFound();
            }

            return View(tableros);
        }

        // POST: Tableros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tableros == null)
            {
                return Problem("Entity set 'DBContext.Tableros'  is null.");
            }
            var tableros = await _context.Tableros.FindAsync(id);
            if (tableros != null)
            {
                _context.Tableros.Remove(tableros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablerosExists(int id)
        {
            return (_context.Tableros?.Any(e => e.IdTablero == id)).GetValueOrDefault();
        }


        [HttpPost]
        public ActionResult generarTableros(int cantidad)
        {
            try
            {
                // Eliminar todos los registros de la tabla Tableros
                _context.Tableros.RemoveRange(_context.Tableros);
                _context.Celdas.RemoveRange(_context.Celdas);
                _context.SaveChanges();

                // Generar una lista de nombres y descripciones únicas para cada tablero
                List<string> nombres = new List<string>();
                List<string> descripciones = new List<string>();
                for (int i = 0; i < cantidad; i++)
                {
                    string nombre = "";
                    string descripcion = "";
                    do
                    {
                        nombre = "Tablero " + (i + 1);
                    } while (nombres.Contains(nombre));
                    nombres.Add(nombre);
                    descripcion = "Descripción del tablero " + (i + 1);
                    descripciones.Add(descripcion);

                    // Verificar si el nombre del tablero ya existe en la base de datos
                    if (!_context.Tableros.Any(t => t.Nombre == nombre))
                    {
                        // Ejecutar el stored procedure para agregar un nuevo registro
                        _context.Database.ExecuteSqlRaw("EXEC spAgregarTablero {0}, {1}", nombre, descripcion);
                    }
                }
                _context.SaveChanges(); // Guardar los cambios en la base de datos
                List<Cartas> cartas = _context.Cartas.ToList();
                Random random = new Random(); // Creamos una instancia de la clase Random


                // Obtener los 4 tableros existentes en la base de datos
                var Tableros = _context.Tableros.ToList();

                foreach (var t in Tableros)
                {
                    List<Cartas> cartasUtilizadas = new List<Cartas>(); // Crear una lista de cartas utilizadas en este tablero
                    for (int i = 0; i < 16; i++)
                    {
                        Cartas cartaAleatoria;
                        do
                        {
                            cartaAleatoria = cartas[random.Next(cartas.Count)]; // Obtener una carta aleatoria de la lista de cartas
                        } while (cartasUtilizadas.Contains(cartaAleatoria)); // Verificar que la carta no se haya utilizado previamente en este tablero
                        cartasUtilizadas.Add(cartaAleatoria); // Agregar la carta utilizada a la lista de cartas utilizadas en este tablero
                        var Id_Carta = cartaAleatoria.IdCarta;
                        int fila = random.Next(1, 5);
                        int columna = random.Next(1, 5);

                        // Ejecutar el stored procedure para agregar una nueva celda
                        _context.Database.ExecuteSqlRaw("EXEC sp_InsertarCelda {0}, {1}, {2}, {3}", Id_Carta, t.IdTablero, fila, columna);
                    }
                }
                return Json(new { success = true, message = "Se generaron los tableros correctamente" });
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí, por ejemplo, registrarla en un archivo de registro o mostrar un mensaje de error al usuario
                return Json(new { success = false, message = "Ocurrió un error al generar los tableros: " + ex.Message });
            }
        }

    }
}
