using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace UI.Models
{
    /// <summary>
    /// Modelo de la capa UI que representa a una persona junto con:
    /// - el listado de departamentos disponibles para seleccionar,
    /// - y un color asociado (normalmente para pintar la fila en la vista).
    /// Se usa para mostrar el “juego” y también para recibir datos en el POST (model binding).
    /// </summary>
    public class PersonaConColor
    {
        /// <summary>
        /// Datos básicos de la persona (DTO) que se mostrarán en la UI y se enviarán en el formulario.
        /// </summary>
        public PersonaDTO _persona { get; set; }

        /// <summary>
        /// Listado de departamentos disponibles para la persona (por ejemplo, para rellenar un &lt;select&gt;).
        /// </summary>
        public List<Departamento> _departamentos { get; set; }

        /// <summary>
        /// Color asignado a la persona (normalmente usado como color de fondo de la fila).
        /// </summary>
        public string _color { get; set; }

        /// <summary>
        /// Constructor vacío requerido por el model binder para enlazar los datos en peticiones POST.
        /// Inicializa colecciones y valores por defecto para evitar referencias nulas.
        /// </summary>
        public PersonaConColor()
        {
            _persona = new PersonaDTO(0, "", "", null);
            _departamentos = new List<Departamento>();
            _color = "";
        }

        /// <summary>
        /// Constructor parametrizado para inicializar el modelo con persona, departamentos y color.
        /// </summary>
        /// <param name="persona">Persona en formato <see cref="PersonaDTO"/>.</param>
        /// <param name="departamentos">Lista de departamentos disponibles.</param>
        /// <param name="color">Color asignado (p. ej., para la fila).</param>
        public PersonaConColor(PersonaDTO persona, List<Departamento> departamentos, string color)
        {
            _persona = persona;
            _departamentos = departamentos;
            _color = color;
        }
    }
}
