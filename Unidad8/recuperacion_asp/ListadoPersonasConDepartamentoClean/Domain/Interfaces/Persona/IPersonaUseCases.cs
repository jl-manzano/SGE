using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define los casos de uso relacionados con personas, incluyendo la obtención de listados y la validación de aciertos por selección de departamento.
    /// </summary>
    public interface IPersonaUseCases
    {
        /// <summary>
        /// Obtiene el listado de personas con su listado de departamentos asociado.
        /// </summary>
        /// <returns>Lista de <see cref="PersonaConListadoDepartamento"/>.</returns>
        List<PersonaConListadoDepartamento> getPersonas();

        /// <summary>
        /// Comprueba el número de aciertos a partir de la selección de departamento realizada para cada persona.
        /// </summary>
        /// <param name="personas">Lista de personas con el departamento seleccionado.</param>
        /// <returns>Número total de aciertos.</returns>
        int comprobarAciertos(List<PersonaConDepartamentoSeleccionado> personas);
    }
}
