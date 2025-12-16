using Domain.Interfaces;
using Domain.Entities;

namespace Domain.UseCases
{
    public class CategoriaUseCases : ICategoriaUseCases
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaUseCases(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<Categoria> getCategorias()
        {
            return _categoriaRepository.getCategorias();
        }

        public string getNombreCategoriaById(int id)
        {
            return _categoriaRepository.getNombreCategoriaById(id);
        }
    }
}
