using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO que contiene una persona y el nombre de su departamento.
    /// </summary>
    public class PersonaWithNombreDepartamentoDTO
    {
        /// <summary>
        /// La persona asociada al DTO.
        /// </summary>
        public Persona persona { get; set; }

        /// <summary>
        /// El nombre del departamento asociado a la persona.
        /// </summary>
        public string nombreDepartamento { get; set; }

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public PersonaWithNombreDepartamentoDTO() { }

        /// <summary>
        /// Constructor con parámetros para inicializar la persona y el nombre del departamento.
        /// </summary>
        /// <param name="persona">La persona asociada al DTO.</param>
        /// <param name="nombreDepartamento">El nombre del departamento asociado.</param>
        public PersonaWithNombreDepartamentoDTO(Persona persona, string nombreDepartamento)
        {
            this.persona = persona;
            this.nombreDepartamento = nombreDepartamento;
        }
    }
}
