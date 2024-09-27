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
using Rotativa.AspNetCore;

namespace AppRepositorioEAPIS.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly BDcontexto _context;
        private readonly ServicioImprimirPractica _servicio;

        public AdministradoresController(BDcontexto context, ServicioImprimirPractica servicio)
        {
            _context = context;
            _servicio = servicio;
        }

        // GET: Administradores
        public async Task<IActionResult> Index(string buscar, string campoBuscar, int pagina = 1)
        {
            var query = _context.Practicas
           .Include(p => p.docente)
           .Include(p => p.empresa)
           .Include(p => p.estudiante)
           .Select(u => u);

            if (!string.IsNullOrEmpty(buscar))
            {
                // Filtro según el campo seleccionado
                switch (campoBuscar)
                {
                    case "Estudiante":
                        query = query.Where(p =>
                             (p.estudiante.Nombres + " " + p.estudiante.Apellidos).Contains(buscar));
                        break;

                    case "Docente":
                        query = query.Where(p =>
                            (p.docente.Nombres + " " + p.docente.Apellidos).Contains(buscar));
                        break;

                    case "Especialidad":
                        query = query.Where(p =>
                            p.Especialidad.Contains(buscar));
                        break;

                    case "Practica":
                        query = query.Where(p =>
                            p.Nombre.Contains(buscar));
                        break;

                    default:
                        break;
                }
            }

            
            var registrosPorPagina = 10;
            var totalRegistros = await query.CountAsync();
            //Paginacion
            var practicas = await query
                .OrderBy(u => u.PracticaID)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            // Creamos del modelo de la vista
            var modelo = new IndexViewModel
            {
                Practicas = practicas,
                PaginaActual = pagina,
                TotalRegistros = totalRegistros,
                RegistroPorPagina = registrosPorPagina,
                Campo_buscar = campoBuscar, // Campo de búsqueda seleccionado
                Termino_buscado = buscar // Término de búsqueda
            };

            return View(modelo);
        }

        // GET: Administradores/Details/5
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

        // GET: Administradores/Create
        public IActionResult Create()
        {
            return RedirectToAction("Create", "Practicas");
        }

        // POST: Administradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocenteID,Nombres,Apellidos,Administrador,Email,Contraseña")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docente);
        }
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["DocenteID"] = new SelectList(_context.Docentes, "DocenteID", "Nombres", practica.DocenteID);
            ViewData["EmpresaID"] = new SelectList(_context.Empresas, "EmpresaID", "Nombre", practica.EmpresaID);
            ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombres", practica.EstudianteID);
            return View(practica);
        }

        // POST: Practicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PracticaID,EstudianteID,DocenteID,Especialidad,EmpresaID,Nombre,NroResolucion,Descripcion,Estado,FechaCreacion,FechaAprobacion,Url,Progreso,Sugerencias")] Practica practica)
        {
            if (id != practica.PracticaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(practica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracticaExists(practica.PracticaID))
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
            ViewData["DocenteID"] = new SelectList(_context.Docentes, "DocenteID", "Nombres", practica.DocenteID);
            ViewData["EmpresaID"] = new SelectList(_context.Empresas, "EmpresaID", "Nombre", practica.EmpresaID);
            ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombres", practica.EstudianteID);
            return View(practica);
        }

        // GET: Administradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var practica = await _context.Practicas.FindAsync(id);
            if (practica != null)
            {
                _context.Practicas.Remove(practica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PracticaExists(int id)
        {
            return _context.Practicas.Any(e => e.PracticaID == id);
        }

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.DocenteID == id);
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Imprimir()
        {
            ViewData["ListaImprmir"] = _servicio.ListaImprimirPractica; //Almacena la listaservicio
            var practicas = _context.Practicas
                .Include(p => p.docente)
                .Include(p => p.empresa)
                .Include(p => p.estudiante)
                .Select(u => u);

            return View(practicas);
        }


        [HttpPost]
        public IActionResult AgregarPractica(int id)
        {
            // Buscar la práctica por ID
            var practica = _context.Practicas
                .Include(p => p.docente)
                .Include(p => p.empresa)
                .Include(p => p.estudiante)
                .FirstOrDefault(p => p.PracticaID == id);

            if (practica != null)
            {
                _servicio.Agregar(practica);
            }
            return RedirectToAction("Imprimir");
        }

        [HttpPost]
        public IActionResult EliminarPractica(int id)
        {
            // Buscar la práctica por ID en la lista 
            var practica = _servicio.ListaImprimirPractica.FirstOrDefault(p => p.PracticaID == id);

            if (practica != null)
            {
                _servicio.ListaImprimirPractica.Remove(practica);
            }

            return RedirectToAction("Imprimir");
        }

        public IActionResult ImprimirPracticas()
        {
            var practicas = _servicio.ListaImprimirPractica
                .Select(p => new PracticasViewModels
                {
                    NombrePractica = p.Nombre,
                    NombreEstudiante = p.estudiante.Nombres + " " + p.estudiante.Apellidos,
                    Especialidad = p.Especialidad,
                    NombreEmpresa = p.empresa.Nombre,
                    Estado = p.Estado,
                    Progreso = p.Progreso
                }).ToList();

            // Si la lista está vacía
            if (!practicas.Any())
            {
                TempData["Mensaje"] = "No hay prácticas seleccionadas para imprimir.";
                return RedirectToAction("Imprimir");
            }

            // Se genera el PDF con Rotativa
            return new ViewAsPdf("ImprimirPracticas", practicas)
            {
                FileName = "Practicas_Seleccionadas.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
