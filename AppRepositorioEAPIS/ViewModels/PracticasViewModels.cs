using AppRepositorioEAPIS.Models;
using System.ComponentModel.DataAnnotations;

namespace AppRepositorioEAPIS.ViewModels
{
    public class PracticasViewModels
    {
        //para trabajar con algunas datos de los modelos 
        //de las practicas para imprimir en el pdf
        public string NombrePractica { get; set; }
        public string NombreEstudiante { get; set; }
        public string Especialidad { get; set; }
        public string NombreEmpresa { get; set; }
        public string Estado { get; set; }
        public int Progreso { get; set; }
    }
}
