using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.UseCases
{
    /// <summary>
    /// Casos de uso relacionados con <see cref="Persona"/>.
    /// Orquesta la obtención de personas desde el repositorio y la obtención de departamentos desde
    /// los casos de uso de departamento, transformando entidades a DTOs para su consumo en capas superiores.
    /// Incluye lógica de negocio para construir el modelo del “juego” y para comprobar los aciertos
    /// comparando selecciones del usuario con los datos reales.
    /// </summary>
    public class PersonaUseCases : IPersonaUseCases
    {
        /// <summary>
        /// Repositorio de personas para acceder a los datos persistidos.
        /// </summary>
        private readonly IPersonaRepository _personaRepository;

        /// <summary>
        /// Casos de uso de departamento para recuperar el catálogo de departamentos.
        /// </summary>
        private readonly IDepartamentoUseCases _departamentoUseCases;

        /// <summary>
        /// Inicializa la clase inyectando las dependencias necesarias.
        /// </summary>
        /// <param name="personaRepository">Repositorio de personas.</param>
        /// <param name="departamentoUseCases">Casos de uso de departamento.</param>
        public PersonaUseCases(IPersonaRepository personaRepository, IDepartamentoUseCases departamentoUseCases)
        {
            _personaRepository = personaRepository;
            _departamentoUseCases = departamentoUseCases;
        }

        /// <summary>
        /// Obtiene las personas y construye una lista preparada para la UI/juego, donde cada persona
        /// (como <see cref="PersonaDTO"/>) se acompaña del listado completo de departamentos disponibles.
        /// Si la persona no tiene departamento asignado (IdDepartamento &lt;= 0), el IdDepartamento del DTO será <c>null</c>.
        /// </summary>
        /// <returns>Lista de <see cref="PersonaConListadoDepartamento"/> para mostrar personas y opciones de departamento.</returns>
        public List<PersonaConListadoDepartamento> getPersonas()
        {
            List<Persona> personasCompletas = _personaRepository.getPersonas();
            List<Departamento> departamentos = _departamentoUseCases.getDepartamentos();

            List<PersonaConListadoDepartamento> resultado = new List<PersonaConListadoDepartamento>();

            for (int i = 0; i < personasCompletas.Count; i++)
            {
                int? idDept = null;

                if (personasCompletas[i].IdDepartamento > 0)
                {
                    idDept = personasCompletas[i].IdDepartamento;
                }

                PersonaDTO personaJuego = new PersonaDTO(
                    personasCompletas[i].Id,
                    personasCompletas[i].Nombre,
                    personasCompletas[i].Apellidos,
                    idDept
                );

                PersonaConListadoDepartamento personaConDepartamentos =
                    new PersonaConListadoDepartamento(personaJuego, departamentos);

                resultado.Add(personaConDepartamentos);
            }

            return resultado;
        }

        /// <summary>
        /// Comprueba cuántas selecciones de departamento realizadas por el usuario son correctas.
        /// Para cada elemento seleccionado, busca la persona real (por Id) y compara su IdDepartamento
        /// con el IdDepartamento del departamento elegido.
        /// </summary>
        /// <param name="personas">Lista de personas con el departamento seleccionado por el usuario.</param>
        /// <returns>Número total de aciertos.</returns>
        // Domain/UseCases/PersonaUseCases.cs  (solo el método comprobarAciertos adaptado)
        // Sin Dictionary y sin continue
        public int comprobarAciertos(List<PersonaConDepartamentoSeleccionado> personas)
        {
            List<Persona> personasReales = _personaRepository.getPersonas();
            int aciertos = 0;

            if (personas != null)
            {
                for (int i = 0; i < personas.Count; i++)
                {
                    PersonaConDepartamentoSeleccionado seleccion = personas[i];
                    Persona personaReal = null;

                    if (seleccion != null)
                    {
                        for (int j = 0; j < personasReales.Count; j++)
                        {
                            if (personasReales[j].Id == seleccion._personaId)
                            {
                                personaReal = personasReales[j];
                            }
                        }

                        if (personaReal != null)
                        {
                            if (personaReal.IdDepartamento == seleccion._departamentoId)
                            {
                                aciertos++;
                            }
                        }
                    }
                }
            }

            return aciertos;
        }

    }
}
