namespace AppRepositorioEAPIS.Models
{
    public class Docente
    {
        public int DocenteID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Administrador {  get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public virtual ICollection<Practica>? Practicas { get; set; }
    }
}
