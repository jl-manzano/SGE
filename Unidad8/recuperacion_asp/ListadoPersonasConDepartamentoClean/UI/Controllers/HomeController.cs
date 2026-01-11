// UI/Controllers/HomeController.cs  (sin var, sin record, sin ViewModel extra, sin Console.WriteLine)
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Mappers;
using UI.Models;

namespace UI.Controllers
{
    /// <summary>
    /// Controlador principal de la aplicación.
    /// Gestiona la vista del “Juego de Departamentos”: carga inicial (GET) y comprobación de respuestas (POST).
    /// El POST recibe únicamente los identificadores (personaId y departamentoId) para evitar model binding de tipos complejos.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Casos de uso de persona: obtención de datos para el juego y comprobación de aciertos.
        /// </summary>
        private readonly IPersonaUseCases _personaUseCases;

        /// <summary>
        /// Mapper de dominio a UI: transforma DTOs del dominio en modelos de UI (incluyendo color por departamento).
        /// </summary>
        private readonly IDomainToUI _mapper;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        /// <param name="personaUseCases">Casos de uso de persona.</param>
        /// <param name="mapper">Mapper de dominio a UI.</param>
        public HomeController(IPersonaUseCases personaUseCases, IDomainToUI mapper)
        {
            _personaUseCases = personaUseCases;
            _mapper = mapper;
        }

        /// <summary>
        /// Acción GET: carga la pantalla inicial del juego.
        /// Obtiene las personas con su listado de departamentos desde la capa de dominio,
        /// las transforma a modelos de UI y devuelve la vista.
        /// </summary>
        /// <returns>Vista Index con la lista de personas preparada para mostrarse.</returns>
        public IActionResult Index()
        {
            List<PersonaConListadoDepartamento> personasConListado = _personaUseCases.getPersonas();
            List<PersonaConColor> personasConColor = new List<PersonaConColor>();

            for (int i = 0; i < personasConListado.Count; i++)
            {
                personasConColor.Add(_mapper.transformar(personasConListado[i]));
            }

            ViewBag.Mensaje = "";
            ViewBag.TipoMensaje = "";

            return View(personasConColor);
        }

        /// <summary>
        /// Acción POST: recibe la selección del usuario como dos listas paralelas:
        /// <c>personaId</c> (ids de persona) y <c>departamentoId</c> (ids de departamento seleccionados).
        /// Con esos datos construye una lista de <see cref="PersonaConDepartamentoSeleccionado"/> (solo IDs),
        /// calcula aciertos llamando a la capa de dominio y recarga el modelo para volver a mostrar la vista.
        /// </summary>
        /// <param name="personaId">Lista de IDs de persona (uno por fila).</param>
        /// <param name="departamentoId">Lista de IDs de departamento seleccionados (uno por fila).</param>
        /// <returns>Vista Index con el mensaje de resultado y el modelo recargado.</returns>
        [HttpPost]
        public IActionResult Index(List<int> personaId, List<int> departamentoId)
        {
            if (personaId == null || departamentoId == null || personaId.Count == 0 || departamentoId.Count == 0)
            {
                ViewBag.Mensaje = "Error: No se recibieron datos del formulario";
                ViewBag.TipoMensaje = "warning";

                List<PersonaConListadoDepartamento> personasConListado = _personaUseCases.getPersonas();
                List<PersonaConColor> personasConColor = new List<PersonaConColor>();

                for (int i = 0; i < personasConListado.Count; i++)
                {
                    personasConColor.Add(_mapper.transformar(personasConListado[i]));
                }

                return View(personasConColor);
            }

            List<PersonaConDepartamentoSeleccionado> seleccionadas = new List<PersonaConDepartamentoSeleccionado>();

            int totalEntradas = personaId.Count;
            if (departamentoId.Count < totalEntradas)
            {
                totalEntradas = departamentoId.Count;
            }

            for (int i = 0; i < totalEntradas; i++)
            {
                if (personaId[i] > 0 && departamentoId[i] > 0)
                {
                    seleccionadas.Add(new PersonaConDepartamentoSeleccionado(personaId[i], departamentoId[i]));
                }
            }

            int aciertos = _personaUseCases.comprobarAciertos(seleccionadas);
            int total = seleccionadas.Count;

            List<PersonaConListadoDepartamento> personasConListadoResult = _personaUseCases.getPersonas();
            List<PersonaConColor> personasConColorResult = new List<PersonaConColor>();

            for (int i = 0; i < personasConListadoResult.Count; i++)
            {
                personasConColorResult.Add(_mapper.transformar(personasConListadoResult[i]));
            }

            if (aciertos == total && total > 0)
            {
                ViewBag.Mensaje = "¡Enhorabuena! ¡Has acertado todos los departamentos! 🎉";
                ViewBag.TipoMensaje = "success";
            }
            else
            {
                ViewBag.Mensaje = $"Has acertado {aciertos} de {total} departamentos. ¡Inténtalo de nuevo!";
                ViewBag.TipoMensaje = "warning";
            }

            return View(personasConColorResult);
        }

        /// <summary>
        /// Acción que devuelve la vista de privacidad.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Acción de error con caché deshabilitada.
        /// Devuelve un modelo con el identificador de la petición para depuración/soporte.
        /// </summary>
        /// <returns>Vista Error con <see cref="ErrorViewModel"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
