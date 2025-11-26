using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.DTOs
{
    public class PersonaWithListadoDepartamentoDTO
    {
        public Persona persona { get; }
        public List<Departamento> departamentos { get; }
        public PersonaWithListadoDepartamentoDTO() { }
        public PersonaWithListadoDepartamentoDTO(Persona persona, List<Departamento> departamentos)
        {
            this.persona = persona;
            this.departamentos = departamentos;
        }


    }
}
