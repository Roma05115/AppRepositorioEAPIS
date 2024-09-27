using System.ComponentModel.DataAnnotations;

namespace AppRepositorioEAPIS.Models
{
    public class Practica
    {
        public int PracticaID{ get; set; }
        [Display(Name = "Estudiante")]
        public int EstudianteID {  get; set; }
        [Display(Name = "Docente")]
        public int DocenteID {  get; set; }
        [Display(Name = "Linea de Especialidad")]
        public string Especialidad {  get; set; }
        [Display(Name = "Empresa")]
        public int EmpresaID {  get; set; }
        [Display(Name = "Nombre practica")]
        public string Nombre {  get; set; }
        [Display(Name = "Número de Resolución")]
        public string NroResolucion {  get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion {  get; set; }
        [Display(Name = "Estado")]
        public string Estado {  get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de Aprobación")]
        public DateTime FechaAprobacion { get; set; }
        [Display(Name = "URL")]
        public string Url {  get; set; }
        [Display(Name = "Progreso")]
        public int Progreso {  get; set; }
        [Display(Name = "Sugerencias")]
        public string Sugerencias {  get; set; }
        public virtual Estudiante? estudiante { get; set; }
        public virtual Docente? docente { get; set; }
        public virtual Empresa?empresa{ get; set; }
    }
}
