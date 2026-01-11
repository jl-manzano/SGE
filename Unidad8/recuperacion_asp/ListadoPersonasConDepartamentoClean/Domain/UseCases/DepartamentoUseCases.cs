using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso relacionados con <see cref="Departamento"/>.
    /// </summary>
    public class DepartamentoUseCases : IDepartamentoUseCases
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DepartamentoUseCases"/>.
        /// </summary>
        /// <param name="departamentoRepository">Repositorio de departamentos utilizado para acceder a los datos.</param>
        public DepartamentoUseCases(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        /// <summary>
        /// Obtiene el listado de departamentos delegando la operación en el repositorio.
        /// </summary>
        /// <returns>Lista de <see cref="Departamento"/>.</returns>
        public List<Departamento> getDepartamentos()
        {
            return _departamentoRepository.getDepartamentos();
        }
    }
}
