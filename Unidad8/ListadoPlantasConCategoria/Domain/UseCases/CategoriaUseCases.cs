using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoriaUseCases : ICategoriaUseCases
    {
        private readonly ICategoriaRepository _categoriaRepository;

        // Constructor que inicializa el repositorio de categorías
        public CategoriaUseCases(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        public List<Categoria> getCategorias()
        {
            return _categoriaRepository.getCategorias();
        }

        /// <summary>
        /// Obtiene una categoría específica por su ID.
        /// </summary>
        public Categoria getCategoria(int id)
        {
            return _categoriaRepository.getCategoria(id);
        }
    }
}
