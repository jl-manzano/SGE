using Domain.DTOs;
using UI.Models;

namespace UI.Mappers
{
    /// <summary>
    /// Interfaz que define el contrato para mapear objetos del dominio a modelos de la capa de UI.
    /// </summary>
    public interface IDomainToUI
    {
        /// <summary>
        /// Transforma una <see cref="PersonaConListadoDepartamento"/> en una <see cref="PersonaConColor"/>,
        /// incorporando la información necesaria para su representación en la UI.
        /// </summary>
        /// <param name="persona">DTO de persona con listado de departamentos.</param>
        /// <returns>Modelo de UI <see cref="PersonaConColor"/> resultante.</returns>
        public PersonaConColor transformar(PersonaConListadoDepartamento persona);
    }
}
