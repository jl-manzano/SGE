using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PersonaWithNombreDepartamentoDTO
    {
        public Persona persona { get; set; }
        public string nombreDepartamento { get; set; }

        public PersonaWithNombreDepartamentoDTO() { }
        public PersonaWithNombreDepartamentoDTO(Persona persona, string nombreDepartamento)
        {
           this.persona = persona;
           this.nombreDepartamento = nombreDepartamento;
        }
    }
}
