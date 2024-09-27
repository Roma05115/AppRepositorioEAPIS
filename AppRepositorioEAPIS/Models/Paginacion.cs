namespace AppRepositorioEAPIS.Models
{
    public class Paginacion
    {
        public int PaginaActual { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistroPorPagina { get; set; }
        public RouteValueDictionary ValoresQueryString { get; set; } //permite trabahjar con Url y rutas
    }
}
