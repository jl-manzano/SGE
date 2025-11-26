using Domain.Entities;
using Domain.Interfaces;
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

        private readonly IDepartamentoRepository _departamentoRepository;

        public PersonaWithListadoDepartamentoDTO() { }
        public PersonaWithListadoDepartamentoDTO(Persona persona, IDepartamentoRepository departamentoRepository)
        {
            this.persona = persona;
            _departamentoRepository = departamentoRepository;
            departamentos = _departamentoRepository.getDepartamentos();
        }


    }
}
