using System.Globalization;

namespace AppRepositorioEAPIS.Models
{
    public class Estudiante
    {
        public int EstudianteID { get; set; }
        public string Nombres {  get; set; }
        public string Apellidos {  get; set; }
        public string Email {  get; set; }
        public string Contraseña { get; set; }
        public virtual ICollection<Practica>? Practicas { get; set; }
    }
}
