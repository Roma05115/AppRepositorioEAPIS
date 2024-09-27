using AppRepositorioEAPIS.Models;


namespace AppRepositorioEAPIS.ViewModels
{
    
    public class IndexViewModel:Paginacion
    {
        public List<Practica> Practicas { get; set; }
        public string Termino_buscado { get; internal set; }
        public string Campo_buscar { get; internal set; }
    }
    
}
