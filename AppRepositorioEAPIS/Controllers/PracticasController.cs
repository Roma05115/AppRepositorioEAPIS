using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRepositorioEAPIS.Data;
using AppRepositorioEAPIS.Models;

namespace AppRepositorioEAPIS.Controllers
{
    public class PracticasController : Controller
    {
        private readonly BDcontexto _context;

        public PracticasController(BDcontexto context)
        {
            _context = context;
        }

        // GET: Practicas
        public async Task<IActionResult> Index()
        {
            var bDcontexto = _context.Practicas.Include(p => p.docente).Include(p => p.empresa).Include(p => p.estudiante);
            return View(await bDcontexto.ToListAsync());
        }

        public async Task<IActionResult> Administrador_Index()
        {
            return RedirectToAction("Index", "Administradores");
        }

        // GET: Practicas/Details/5
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

        // GET: Practicas/Create
        public IActionResult Create()
        {
            ViewData["DocenteID"] = new SelectList(_context.Docentes, "DocenteID", "Nombres");
            ViewData["EmpresaID"] = new SelectList(_context.Empresas, "EmpresaID", "Nombre");
            ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombres");
            return View();
        }

        // POST: Practicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PracticaID,EstudianteID,DocenteID,Especialidad,EmpresaID,Nombre,NroResolucion,Descripcion,Estado,FechaCreacion,FechaAprobacion,Url,Progreso,Sugerencias")] Practica practica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(practica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Administradores");
            }
            ViewData["DocenteID"] = new SelectList(_context.Docentes, "DocenteID", "Nombres", practica.DocenteID);
            ViewData["EmpresaID"] = new SelectList(_context.Empresas, "EmpresaID", "Nombre", practica.EmpresaID);
            ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombres", practica.EstudianteID);
            return View(practica);
        }

        // GET: Practicas/Edit/5
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

        // GET: Practicas/Delete/5
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

        // POST: Practicas/Delete/5
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

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
