using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTOs;

namespace Domain.UseCases
{
    public class PersonaUseCases: IPersonaUseCases
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public PersonaUseCases(IPersonaRepository personaRepository, IDepartamentoRepository departamentoRepository)
        {
            _personaRepository = personaRepository;
            _departamentoRepository = departamentoRepository;
        }

        public List<Persona> getListadoPersonas()
        {
            return _personaRepository.getPersonas();
        }

        public List<Departamento> getDepartamentos()
        {
            return _departamentoRepository.getDepartamentos();
        }

        public PersonaWithNombreDepartamentoDTO getPersona(int id)
        {
            // Obtenemos la persona
            Persona persona = _personaRepository.getPersona(id);

            // Obtenemos el nombre del departamento
            string nombreDepartamento = _departamentoRepository.getDepartamento(persona.IdDepartamento).NombreDepartamento;

            // Creamos el DTO
            PersonaWithNombreDepartamentoDTO personaDTO = new PersonaWithNombreDepartamentoDTO(persona, nombreDepartamento);

            // Devolvemos el DTO
            return personaDTO;
        }

        public List<PersonaWithNombreDepartamentoDTO> getPersonas()
        {
            // Creamos el listado a devolver
            List<PersonaWithNombreDepartamentoDTO> listadoDTOs = new List<PersonaWithNombreDepartamentoDTO>();

            // Obtenemos el listado de personas
            List<Persona> personas = _personaRepository.getPersonas();

            // Recorremos el listado de personas y mapeamos
            foreach (Persona persona in personas)
            {
                // Obtenemos el nombre del departamento
                string nombreDepartamento = _departamentoRepository.getDepartamento(persona.IdDepartamento).NombreDepartamento;

                // Creamos el DTO
                PersonaWithNombreDepartamentoDTO personaDTO = new PersonaWithNombreDepartamentoDTO(persona, nombreDepartamento);

                // Añadimos el DTO a la lista
                listadoDTOs.Add(personaDTO);
            }

            // Devolvemos el listado
            return listadoDTOs;
        }

        public PersonaWithListadoDepartamentoDTO GetPersonaWithListadoDepartamento(int id)
        {
            // Creamos el DTO
            PersonaWithListadoDepartamentoDTO personaListado = new PersonaWithListadoDepartamentoDTO(_personaRepository.getPersona(id), _departamentoRepository.getDepartamentos());

            // Devolvemos el listado
            return personaListado;

        }

        public int addPersona(Persona persona)
        {
            return _personaRepository.addPersona(persona);
        }

        public int updatePersona(int id, Persona persona)
        {
            return _personaRepository.updatePersona(id, persona);
        }

        public int deletePersona(int id)
        {
            return _personaRepository.deletePersona(id);
        }

    }
}
