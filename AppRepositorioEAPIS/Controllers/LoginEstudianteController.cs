using Microsoft.AspNetCore.Mvc;
using AppRepositorioEAPIS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using AppRepositorioEAPIS.Data;


namespace AppRepositorioEAPIS.Controllers
{
    public class LoginEstudianteController : Controller
    {
        private readonly BDcontexto _context;
        public LoginEstudianteController(BDcontexto context)
        {
            _context = context;
        }

        public IActionResult LoginEstudiante()
        {
            return View();
        }
        public async Task<IActionResult> inicio()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult LoginEstudiante(Estudiante oEstudiante)
        {
            //establecemos conec con la BD
            var connection = _context.Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "sp_ValidarEstudiante";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //parámetros
                var emailParam = cmd.CreateParameter();
                emailParam.ParameterName = "@Correo";
                emailParam.Value = oEstudiante.Email;
                cmd.Parameters.Add(emailParam);

                var passwordParam = cmd.CreateParameter();
                passwordParam.ParameterName = "@Contraseña";
                passwordParam.Value = oEstudiante.Contraseña;
                cmd.Parameters.Add(passwordParam);

                //conex
                connection.Open();
                //ejecutamos el PA y lo convertimos en un entero
                var estudianteId = Convert.ToInt32(cmd.ExecuteScalar());

                if (estudianteId != 0)
                {
                    oEstudiante.EstudianteID = estudianteId;
                    // Guardar el objeto del docente en sesión
                    HttpContext.Session.SetInt32("EstudianteID", oEstudiante.EstudianteID);
                    HttpContext.Session.SetString("EstudianteEmail", oEstudiante.Email);
                    return RedirectToAction("Index", "Estudiantes");
                }
                else
                {
                    ViewData["Mensaje"] = "Estudiante no encontrado";
                    return View();
                }
            }
        }
    }
}
