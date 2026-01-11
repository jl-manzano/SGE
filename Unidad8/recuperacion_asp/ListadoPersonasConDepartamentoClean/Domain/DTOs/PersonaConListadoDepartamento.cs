using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO que agrupa una persona (en formato <see cref="PersonaDTO"/>) junto con un listado de departamentos.
    /// Se utiliza para transportar la persona y las opciones de departamentos disponibles (por ejemplo, para
    /// mostrarlas en un desplegable en la UI o para procesos de asignación).
    /// </summary>
    public class PersonaConListadoDepartamento
    {
        /// <summary>
        /// Datos de la persona.
        /// </summary>
        public PersonaDTO _persona { get; }

        /// <summary>
        /// Listado de departamentos disponibles para la persona.
        /// </summary>
        public List<Departamento> _listadoDepartamentos { get; }

        /// <summary>
        /// Inicializa el DTO con la persona y el listado de departamentos.
        /// </summary>
        /// <param name="persona">Persona en formato <see cref="PersonaDTO"/>.</param>
        /// <param name="listadoDepartamentos">Lista de departamentos disponibles.</param>
        public PersonaConListadoDepartamento(PersonaDTO persona, List<Departamento> listadoDepartamentos)
        {
            _persona = persona;
            _listadoDepartamentos = listadoDepartamentos;
        }
    }
}
