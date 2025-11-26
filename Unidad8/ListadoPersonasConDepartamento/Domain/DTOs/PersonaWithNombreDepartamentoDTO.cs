using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PersonaWithNombreDepartamentoDTO
    {
        public Persona persona { get; }
        public string nombreDepartamento { get; }
        private readonly IDepartamentoRepository _departamentoRepository;

        public PersonaWithNombreDepartamentoDTO() { }
        public PersonaWithNombreDepartamentoDTO(Persona persona, IDepartamentoRepository departamentoRepository)
        {
           this.persona = persona;
           _departamentoRepository = departamentoRepository;
           var nombreDepartamento = _departamentoRepository.getDepartamento(persona.IdDepartamento).NombreDepartamento;
        }
    }
}
