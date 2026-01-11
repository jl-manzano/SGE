// Domain/DTOs/PersonaConDepartamentoSeleccionado.cs
namespace Domain.DTOs
{
    /// <summary>
    /// DTO que representa la selección de un departamento para una persona.
    /// Transporta únicamente los identificadores necesarios para comprobar aciertos.
    /// </summary>
    public class PersonaConDepartamentoSeleccionado
    {
        public int _personaId { get; }
        public int _departamentoId { get; }

        public PersonaConDepartamentoSeleccionado(int personaId, int departamentoId)
        {
            _personaId = personaId;
            _departamentoId = departamentoId;
        }
    }
}
