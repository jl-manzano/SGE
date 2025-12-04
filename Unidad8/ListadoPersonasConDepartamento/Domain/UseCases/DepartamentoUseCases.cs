using Domain.Interfaces;
using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.UseCases
{
    /// <summary>
    /// Casos de uso relacionados con los departamentos.
    /// </summary>
    public class DepartamentoUseCases : IDepartamentoUseCases
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        /// <summary>
        /// Constructor que inicializa el repositorio de departamentos.
        /// </summary>
        /// <param name="departamentoRepository">Repositorio de departamentos.</param>
        public DepartamentoUseCases(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
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
        /// Obtiene un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento.</param>
        /// <returns>Departamento con el ID especificado.</returns>
        public Departamento getDepartamento(int id)
        {
            return _departamentoRepository.getDepartamento(id);
        }

        /// <summary>
        /// Añade un nuevo departamento.
        /// </summary>
        /// <param name="departamento">El departamento a añadir.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int addDepartamento(Departamento departamento)
        {
            return _departamentoRepository.addDepartamento(departamento);
        }

        /// <summary>
        /// Actualiza los datos de un departamento.
        /// </summary>
        /// <param name="id">ID del departamento a actualizar.</param>
        /// <param name="departamento">Nuevo objeto de departamento con los datos actualizados.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int updateDepartamento(int id, Departamento departamento)
        {
            return _departamentoRepository.updateDepartamento(id, departamento);
        }

        /// <summary>
        /// Elimina un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>
        public int deleteDepartamento(int id)
        {
            return _departamentoRepository.deleteDepartamento(id);
        }
    }
}
