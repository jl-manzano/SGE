using Domain.Interfaces;
using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.DTOs;

namespace Domain.UseCases
{
    /// <summary>
    /// Casos de uso relacionados con las personas.
    /// </summary>
    public class PersonaUseCases : IPersonaUseCases
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        /// <summary>
        /// Constructor que inicializa el repositorio de personas y departamentos.
        /// </summary>
        /// <param name="personaRepository">Repositorio de personas.</param>
        /// <param name="departamentoRepository">Repositorio de departamentos.</param>
        public PersonaUseCases(IPersonaRepository personaRepository, IDepartamentoRepository departamentoRepository)
        {
            _personaRepository = personaRepository;
            _departamentoRepository = departamentoRepository;
        }

        /// <summary>
        /// Obtiene la lista de todas las personas.
        /// </summary>
        /// <returns>Lista de personas.</returns>
        public List<Persona> getListadoPersonas()
        {
            return _personaRepository.getPersonas();
        }

        /// <summary>
        /// Obtiene la lista de todos los departamentos.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        public List<Departamento> getDepartamentos()
        {
            return _departamentoRepository.getDepartamentos();
        }

        /// <summary>
        /// Obtiene los detalles de una persona junto con el nombre de su departamento.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>DTO con los detalles de la persona y el nombre de su departamento.</returns>
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

        /// <summary>
        /// Obtiene la lista de todas las personas con los nombres de sus departamentos.
        /// </summary>
        /// <returns>Lista de DTOs con las personas y sus departamentos.</returns>
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

        /// <summary>
        /// Obtiene una persona junto con un listado de todos los departamentos.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>DTO con la persona y los departamentos.</returns>
        public PersonaWithListadoDepartamentoDTO GetPersonaWithListadoDepartamento(int id)
        {
            // Creamos el DTO
            PersonaWithListadoDepartamentoDTO personaListado = new PersonaWithListadoDepartamentoDTO(_personaRepository.getPersona(id), _departamentoRepository.getDepartamentos());

            // Devolvemos el listado
            return personaListado;
        }

        /// <summary>
        /// Añade una nueva persona.
        /// </summary>
        /// <param name="persona">La persona a añadir.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int addPersona(Persona persona)
        {
            return _personaRepository.addPersona(persona);
        }

        /// <summary>
        /// Actualiza los datos de una persona.
        /// </summary>
        /// <param name="id">ID de la persona a actualizar.</param>
        /// <param name="persona">Nuevo objeto de persona con los datos actualizados.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int updatePersona(int id, Persona persona)
        {
            return _personaRepository.updatePersona(id, persona);
        }

        /// <summary>
        /// Elimina una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int deletePersona(int id)
        {
            return _personaRepository.deletePersona(id);
        }
    }
}
