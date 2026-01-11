using System;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que transporta datos básicos de una persona entre capas.
    /// No representa comportamiento de dominio; únicamente expone valores (propiedades).
    /// </summary>
    public class PersonaDTO
    {
        #region Propiedades públicas

        /// <summary>
        /// Identificador único de la persona.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Nombre de la persona.
        /// </summary>
        public string Nombre { get; }

        /// <summary>
        /// Apellidos de la persona.
        /// </summary>
        public string Apellidos { get; }

        /// <summary>
        /// Identificador del departamento asociado a la persona.
        /// Puede ser <c>null</c> si no tiene departamento asignado.
        /// </summary>
        public int? IdDepartamento { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa el DTO con todos sus datos.
        /// </summary>
        /// <param name="id">Identificador de la persona.</param>
        /// <param name="nombre">Nombre.</param>
        /// <param name="apellidos">Apellidos.</param>
        /// <param name="idDepartamento">Id del departamento (nullable).</param>
        public PersonaDTO(int id, string nombre, string apellidos, int? idDepartamento)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            IdDepartamento = idDepartamento;
        }

        #endregion
    }
}
