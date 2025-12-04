using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz para los casos de uso relacionados con los departamentos.
    /// </summary>
    public interface IDepartamentoUseCases
    {
        /// <summary>
        /// Obtiene la lista de todos los departamentos.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        List<Departamento> getDepartamentos();

        /// <summary>
        /// Obtiene un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento.</param>
        /// <returns>Un departamento con el ID especificado.</returns>
        Departamento getDepartamento(int id);

        /// <summary>
        /// Añade un nuevo departamento.
        /// </summary>
        /// <param name="departamento">El departamento a añadir.</param>
        /// <returns>El número de filas afectadas.</returns>
        int addDepartamento(Departamento departamento);

        /// <summary>
        /// Actualiza los datos de un departamento.
        /// </summary>
        /// <param name="id">ID del departamento a actualizar.</param>
        /// <param name="departamento">Nuevo objeto de departamento con los datos actualizados.</param>
        /// <returns>El número de filas afectadas.</returns>
        int updateDepartamento(int id, Departamento departamento);

        /// <summary>
        /// Elimina un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>
        int deleteDepartamento(int id);
    }
}
