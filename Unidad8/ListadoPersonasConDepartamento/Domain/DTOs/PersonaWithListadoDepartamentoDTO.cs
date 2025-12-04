using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO que contiene una persona y un listado de departamentos.
    /// </summary>
    public class PersonaWithListadoDepartamentoDTO
    {
        /// <summary>
        /// La persona asociada al DTO.
        /// </summary>
        public Persona persona { get; set; }

        /// <summary>
        /// Lista de departamentos asociados a la persona.
        /// </summary>
        public List<Departamento> departamentos { get; set; }

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public PersonaWithListadoDepartamentoDTO() { }

        /// <summary>
        /// Constructor con parámetros para inicializar la persona y los departamentos.
        /// </summary>
        /// <param name="persona">La persona asociada al DTO.</param>
        /// <param name="departamentos">La lista de departamentos asociados.</param>
        public PersonaWithListadoDepartamentoDTO(Persona persona, List<Departamento> departamentos)
        {
            this.persona = persona;
            this.departamentos = departamentos;
        }
    }
}
