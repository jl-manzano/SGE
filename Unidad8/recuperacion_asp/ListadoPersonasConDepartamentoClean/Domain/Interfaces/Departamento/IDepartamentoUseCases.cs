using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define los casos de uso relacionados con la entidad <see cref="Departamento"/>.
    /// </summary>
    public interface IDepartamentoUseCases
    {
        /// <summary>
        /// Obtiene el listado de departamentos.
        /// </summary>
        /// <returns>Lista de <see cref="Departamento"/>.</returns>
        List<Departamento> getDepartamentos();

    }
}
