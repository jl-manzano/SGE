using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Planta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int IdCategoria { get; set; }

        public Planta() { }

        public Planta(int id, string nombre, string descripcion, double precio, int idCategoria)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            IdCategoria = idCategoria;
        }
    }
}
