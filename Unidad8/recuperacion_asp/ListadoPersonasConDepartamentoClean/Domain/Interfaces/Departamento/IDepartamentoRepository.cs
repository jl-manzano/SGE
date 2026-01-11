using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del repositorio de departamentos para exponer operaciones de acceso a datos sobre la entidad <see cref="Departamento"/>.
    /// </summary>
    public interface IDepartamentoRepository
    {
        /// <summary>
        /// Devuelve el listado de departamentos disponibles.
        /// </summary>
        /// <returns>Lista de <see cref="Departamento"/>.</returns>
        List<Departamento> getDepartamentos();
    }
}
