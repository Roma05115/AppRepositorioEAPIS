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
    public class EstudiantesController : Controller
    {
        private readonly BDcontexto _context;

        public EstudiantesController(BDcontexto context)
        {
            _context = context;
        }

        public IActionResult ImprimirPracticas()
        {
            var estudiantepracId = HttpContext.Session.GetInt32("EstudianteID");

            if (estudiantepracId == null)
            {
                return RedirectToAction("LoginEstudiante", "LoginEstudiante");
            }
            var practicas = _context.Practicas
                .Include(p => p.estudiante)
                .Include(p => p.empresa)
                .Where(p => p.EstudianteID == estudiantepracId)
                .Select(p => new PracticasViewModels
                {
                    NombrePractica = p.Nombre,
                    NombreEstudiante = p.estudiante.Nombres + " " + p.estudiante.Apellidos,
                    Especialidad = p.Especialidad,
                    NombreEmpresa = p.empresa.Nombre,
                    Estado = p.Estado,
                    Progreso = p.Progreso
                }).ToList();
    
            ViewBag.EstudianteID = estudiantepracId;
            return new ViewAsPdf("ImprimirPracticas", practicas)
            {
                FileName = $"Practicas_Estudiante_{estudiantepracId}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        // GET: Estudiantes
        public async Task<IActionResult> Index(string buscar, string campoBuscar, int pagina = 1)
        {
            var query = _context.Practicas
           .Include(p => p.docente)
           .Include(p => p.empresa)
           .Include(p => p.estudiante)
           .Where(p => p.Progreso == 100);

            if (!string.IsNullOrEmpty(buscar))
            {
                // filtro según el campo seleccionado
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

            //paginación
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
                RegistroPorPagina = registrosPorPagina,
                 Campo_buscar = campoBuscar, // Campo de búsqueda seleccionado
                Termino_buscado = buscar // Término de búsqueda
            };

            // retorno de la vista con el modelo de datos
            return View(modelo);
        }

        // GET: Estudiantes/Details/5
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

        public async Task<IActionResult> MisPracticas()
        {

            var estudianteId = HttpContext.Session.GetInt32("EstudianteID");


            if (estudianteId == null)
            {
                return RedirectToAction("LoginEstudiante", "LoginEstudiante");
            }

            // las prácticas relacionadas con estudiantes
            var practicas = await _context.Practicas
                .Include(p => p.docente)
                .Include(p => p.empresa)
                .Include(p => p.estudiante)
                .Where(p => p.EstudianteID == estudianteId) // filtra por el estudianteId
                .ToListAsync();

            // si no hay prácticas  retornar un mensaje 
            if (!practicas.Any())
            {
                ViewData["Mensaje"] = "No tienes prácticas";
            }

            return View(practicas);
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
