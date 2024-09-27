namespace AppRepositorioEAPIS.Models
{
    public class Empresa
    {
        public int EmpresaID { get; set; }
        public string Nombre { get; set; }
        public string Direccion {  get; set; }
        public string Telefono {  get; set; }
        public string Email { get; set; }
        public virtual ICollection<Practica>? Practicas { get; set; }
    }
}
