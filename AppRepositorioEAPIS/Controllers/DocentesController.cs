using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRepositorioEAPIS.Data;
using AppRepositorioEAPIS.Models;
using AppRepositorioEAPIS.ViewModels;

namespace AppRepositorioEAPIS.Controllers
{
    public class DocentesController : Controller
    {
        private readonly BDcontexto _context;

        public DocentesController(BDcontexto context)
        {
            _context = context;
        }


        // GET: Docentes
        public async Task<IActionResult> Index(int pagina = 1)
        {
            var query = _context.Practicas
            .Include(p => p.docente)
            .Include(p => p.empresa)
            .Include(p => p.estudiante)
            .Where(p => p.Progreso == 100);

            var registrosPorPagina = 10;
            var totalRegistros = await query.CountAsync();
            var practicas = await query
                .OrderBy(u => u.PracticaID)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            // modelo de la vista
            var modelo = new IndexViewModel
            {
                Practicas = practicas,
                PaginaActual = pagina,
                TotalRegistros = totalRegistros,
                RegistroPorPagina = registrosPorPagina
            };

            return View(modelo);
        }

        public async Task<IActionResult> Asesorias()
        {
            var docenteId = HttpContext.Session.GetInt32("DocenteID");

            // Si no hay un docente redirigir al login
            if (docenteId == null)
            {
                return RedirectToAction("LoginDocente", "LoginDocente");
            }

            // obtener las prácticasd el docente logueado
            var practicas = await _context.Practicas
                .Include(p => p.docente)
                .Include(p => p.empresa)
                .Include(p => p.estudiante)
                .Where(p => p.DocenteID == docenteId) // Filtrar por el DocenteID
                .ToListAsync();

            if (!practicas.Any())
            {
                ViewData["Mensaje"] = "No tienes prácticas asignadas.";
            }

            return View(practicas);
        }


        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practica = await _context.Practicas
                .Include(p => p.docente)
                .Include(p => p.empresa)
                .Include(p => p.estudiante)
                .FirstOrDefaultAsync(m => m.PracticaID == id);
            if (practica == null)
            {
                return NotFound();
            }

            return View(practica);
        }

        // GET: Practicas/Edit/5
        public async Task<IActionResult> sugerencias(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practica = await _context.Practicas.FindAsync(id);
            if (practica == null)
            {
                return NotFound();
            }

            return View(practica); // Se devuelve el objeto práctica con su ID
        }

        // POST: Practicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Practicas/Edit/5
        public async Task<IActionResult> sugerencias(int id, [Bind("Progreso,Sugerencias")] Practica practica)
        {
            // Buscar la práctica original en la base de datos y verificar si existe
            var practicaOriginal = await _context.Practicas.FindAsync(id);
            

            if (practicaOriginal == null)
            {
                return NotFound();
            }

            // Si el estado del modelo es válido, procede
            try
            {
                // Busca la práctica original en la base de datos
                if (practicaOriginal == null)
                {
                    return NotFound();
                }

                // Actualiza únicamente los campos "Progreso" y "Sugerencias"
                practicaOriginal.Progreso = practica.Progreso;
                practicaOriginal.Sugerencias = practica.Sugerencias;

                // guarda los cambios
                await _context.SaveChangesAsync();

                // redirige a la vista de asesorías 
                return RedirectToAction("Asesorias");
            }
            catch (DbUpdateConcurrencyException)
            {
                // maneja problemas de concurrencia
                if (!PracticaExists(practica.PracticaID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewBag.Errores = errores;
                return View(practica);
            }
            // Si el modelo no es válido, devuelve los datos a la vista
            return View(practica);
        }


        /*
          if (!ModelState.IsValid)
            {
                // Obtener todos los errores del ModelState
                var errores = ModelState.Values.SelectMany(v => v.Errors);

                foreach (var error in errores)
                {
                    Console.WriteLine(error.ErrorMessage); // Imprimir los mensajes de error en consola o log
                }

                // O también podrías usar TempData o ViewBag para mostrar los errores en la vista
                TempData["ErrorMensaje"] = "Hay errores en los datos enviados, por favor revisa e inténtalo de nuevo.";

                // Devolver la vista con los errores
                return View(practica);
            }
*/

        private bool PracticaExists(int id)
        {
            return _context.Practicas.Any(e => e.PracticaID == id);
        }

        // GET: Docentes/Edit/5

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.DocenteID == id);
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); //Retorna a la vista
        }

    }
}
