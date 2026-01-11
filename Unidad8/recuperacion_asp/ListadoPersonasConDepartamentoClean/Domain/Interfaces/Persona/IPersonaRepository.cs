using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del repositorio de personas para exponer operaciones de acceso a datos sobre la entidad <see cref="Persona"/>.
    /// </summary>
    public interface IPersonaRepository
    {
        /// <summary>
        /// Obtiene el listado de personas.
        /// </summary>
        /// <returns>Lista de <see cref="Persona"/>.</returns>
        List<Persona> getPersonas();
    }
}
