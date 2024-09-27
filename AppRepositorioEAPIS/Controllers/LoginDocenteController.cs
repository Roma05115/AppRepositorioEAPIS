using Microsoft.AspNetCore.Mvc;
using AppRepositorioEAPIS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using AppRepositorioEAPIS.Data;

namespace AppRepositorioEAPIS.Controllers
{
    public class LoginDocenteController : Controller
    {
        private readonly BDcontexto _context;

        public LoginDocenteController(BDcontexto context)
        {
            _context = context;
        }
        public IActionResult LoginDocente()
        {
            return View();
        }

        public async Task<IActionResult> inicio()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult LoginDocente(Docente oDocente)
        {
            //conec. con la BD
            var connection = _context.Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "sp_ValidarDocente";
                cmd.CommandType = CommandType.StoredProcedure;

                // Añadir los parámetros
                var emailParam = cmd.CreateParameter();
                emailParam.ParameterName = "@Correo";
                emailParam.Value = oDocente.Email;
                cmd.Parameters.Add(emailParam);

                var passwordParam = cmd.CreateParameter();
                passwordParam.ParameterName = "@Contraseña";
                passwordParam.Value = oDocente.Contraseña;
                cmd.Parameters.Add(passwordParam);

                // Abrir conexión
                connection.Open();

                // Ejecutar el procedimiento almacenado y leer los resultados
                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var docenteId = reader.GetInt32(0); // Primer valor DocenteID lo sacamos del PA
                        var tipodecolumnaadministrador = reader.GetFieldType(1); ; // Segundo valor Administrador

                        if (docenteId != 0)
                        {
                            // verigicamos que el tipo de dato que tenemos en BD
                            
                            oDocente.DocenteID = docenteId;
                            if (tipodecolumnaadministrador == typeof(bool))
                            {
                                oDocente.Administrador = reader.GetBoolean(1); // Segundo valor Administrador
                            }
                            else if (tipodecolumnaadministrador == typeof(int))
                            {
                                oDocente.Administrador = reader.GetInt32(1) != 0; // Convertir el int en bool
                            }

                            // Guardar en la sesión el ID, el email y el estado de administrador
                            HttpContext.Session.SetInt32("DocenteID", oDocente.DocenteID);
                            HttpContext.Session.SetString("DocenteEmail", oDocente.Email);
                            HttpContext.Session.SetString("DocenteAdmin", oDocente.Administrador.ToString());

                            // Verificar si el docente es administrador
                            if (oDocente.Administrador)
                            {
                                return RedirectToAction("Index", "Administradores");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Docentes");
                            }
                        }
                        else
                        {
                            ViewData["Mensaje"] = "Docente no encontrado";
                            return View();
                        }
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Error en la autenticación";
                        return View();
                    }
                }
            }
        }


    }
}
