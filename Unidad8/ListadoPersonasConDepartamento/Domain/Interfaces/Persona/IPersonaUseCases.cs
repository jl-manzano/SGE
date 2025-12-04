using System;
using System.Collections.Generic;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz para los casos de uso relacionados con las personas.
    /// </summary>
    public interface IPersonaUseCases
    {
        /// <summary>
        /// Obtiene la lista de todas las personas.
        /// </summary>
        /// <returns>Lista de personas.</returns>
        List<Persona> getListadoPersonas();

        /// <summary>
        /// Obtiene la lista de todos los departamentos.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        List<Departamento> getDepartamentos();

        /// <summary>
        /// Obtiene los detalles de una persona junto con el nombre de su departamento.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>DTO con los detalles de la persona y el nombre de su departamento.</returns>
        PersonaWithNombreDepartamentoDTO getPersona(int id);

        /// <summary>
        /// Obtiene la lista de todas las personas con los nombres de sus departamentos.
        /// </summary>
        /// <returns>Lista de DTOs con las personas y sus departamentos.</returns>
        List<PersonaWithNombreDepartamentoDTO> getPersonas();

        /// <summary>
        /// Obtiene una persona junto con un listado de todos los departamentos.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>DTO con la persona y los departamentos.</returns>
        PersonaWithListadoDepartamentoDTO GetPersonaWithListadoDepartamento(int id);

        /// <summary>
        /// Añade una nueva persona.
        /// </summary>
        /// <param name="persona">La persona a añadir.</param>
        /// <returns>El número de filas afectadas.</returns>
        int addPersona(Persona persona);

        /// <summary>
        /// Actualiza los datos de una persona.
        /// </summary>
        /// <param name="id">ID de la persona a actualizar.</param>
        /// <param name="persona">Nuevo objeto de persona con los datos actualizados.</param>
        /// <returns>El número de filas afectadas.</returns>
        int updatePersona(int id, Persona persona);

        /// <summary>
        /// Elimina una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>
        int deletePersona(int id);
    }
}
