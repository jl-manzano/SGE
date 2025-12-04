using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones relacionadas con las personas en la base de datos.
    /// </summary>
    public interface IPersonaRepository
    {
        /// <summary>
        /// Obtiene la lista de todas las personas.
        /// </summary>
        /// <returns>Lista de personas.</returns>
        List<Persona> getPersonas();

        /// <summary>
        /// Obtiene una persona por su ID.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>Una persona con el ID especificado.</returns>
        Persona getPersona(int id);

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
