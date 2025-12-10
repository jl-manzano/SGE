using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        // Simulamos una lista en memoria que reemplaza la base de datos
        private static List<Categoria> _categorias = new List<Categoria>
        {
            new Categoria(1, "Medicinales"),
            new Categoria(2, "Decorativas"),
            new Categoria(3, "Exóticas")
        };

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        public List<Categoria> getCategorias()
        {
            // Retorna la lista simulada de categorías
            return _categorias;
        }

        /// <summary>
        /// Obtiene una categoría específica por su ID.
        /// </summary>
        public Categoria getCategoria(int id)
        {
            // Busca la categoría por su ID en la lista en memoria
            return _categorias.FirstOrDefault(c => c.Id == id);
        }
    }
}
