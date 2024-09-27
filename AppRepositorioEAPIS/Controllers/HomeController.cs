using AppRepositorioEAPIS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppRepositorioEAPIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); //Pagina principal
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login_docente()
        {
            return RedirectToAction("LoginDocente", "LoginDocente"); //Redireccion a la pagina del logi ndocente
        }

        public IActionResult Login_estudiantes()
        {
            return RedirectToAction("LoginEstudiante", "LoginEstudiante"); //Redireccion a la pagina del login estudiante
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
