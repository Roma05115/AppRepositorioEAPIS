using AppRepositorioEAPIS.Models;

namespace AppRepositorioEAPIS.Data
{
    public class ServicioImprimirPractica
    {
        public List<Practica> ListaImprimirPractica { get; set; } =
            new List<Practica>();
        public void Agregar(Practica _practica)
        {
            ListaImprimirPractica.Add(_practica);
        }
        public void Eliminar(int i)
        {
           //elimina un elemento en una posicion i
            ListaImprimirPractica.RemoveAt(i);
        }
        public void EliminarTodos()
        {
            ListaImprimirPractica.Clear();
        }
    }
}
