using Domain.DTOs;
using UI.Models;
using System.Collections.Generic;

namespace UI.Mappers
{
    /// <summary>
    /// Mapper que transforma modelos/DTOs de la capa de Dominio a modelos específicos de la UI.
    /// En este caso, asigna un color de fila a cada persona en función de su departamento para
    /// facilitar la representación visual (por ejemplo, en el "juego" de departamentos).
    /// </summary>
    public class DomainToUI : IDomainToUI
    {
        /// <summary>
        /// Lista de colores disponibles para asignar a las filas, indexados por departamento.
        /// El índice 0 corresponde al departamento con Id = 1, el índice 1 al Id = 2, etc.
        /// </summary>
        private readonly List<string> _colores;

        /// <summary>
        /// Inicializa el mapper con una paleta fija de colores para la UI.
        /// </summary>
        public DomainToUI()
        {
            _colores = new List<string>
            {
                "#FFE5E5",
                "#E5F3FF",
                "#FFF5E5",
                "#E5FFE5"
            };
        }

        /// <summary>
        /// Convierte un <see cref="PersonaConListadoDepartamento"/> (dominio) en un <see cref="PersonaConColor"/> (UI),
        /// calculando el color a partir del Id de departamento.
        /// Si el IdDepartamento es nulo o está fuera de rango, se asigna el color por defecto (índice 0).
        /// </summary>
        /// <param name="persona">Objeto de dominio que contiene la persona y el listado de departamentos.</param>
        /// <returns>Instancia de <see cref="PersonaConColor"/> con el color asignado.</returns>
        public PersonaConColor transformar(PersonaConListadoDepartamento persona)
        {
            int indiceDepartamento = 0;

            if (persona != null &&
                persona._persona != null &&
                persona._persona.IdDepartamento.HasValue &&
                persona._persona.IdDepartamento.Value >= 1)
            {
                indiceDepartamento = persona._persona.IdDepartamento.Value - 1;
            }

            if (indiceDepartamento < 0 || indiceDepartamento >= _colores.Count)
            {
                indiceDepartamento = 0;
            }

            string color = _colores[indiceDepartamento];

            return new PersonaConColor(persona._persona, persona._listadoDepartamentos, color);
        }
    }
}
